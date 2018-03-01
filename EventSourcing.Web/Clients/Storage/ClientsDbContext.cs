using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcing.Web.Clients.Domain.Clients;
using EventSourcing.Web.ClientsContracts.Queries;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using ReflectionMagic;
using StackExchange.Redis;

namespace EventSourcing.Web.Clients.Storage
{
    public abstract class ClientsDbContext
    {
        private readonly IConnectionMultiplexer _redisConnection;

        private readonly string _namespace;

        public ClientsDbContext(IConnectionMultiplexer redis, string nameSpace)
        {
            _redisConnection = redis;
            _namespace = nameSpace;
        }

        public T Get<T>(Guid id)
        {
            return Get<T>(id.ToString());
        }

        public T Get<T>(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            if (serializedObject.IsNullOrEmpty) throw new ArgumentNullException();
            return JsonConvert.DeserializeObject<T>(serializedObject.ToString());
        }

        public List<T> GetAll<T>()
        {
            var database = _redisConnection.GetDatabase();
            var endpoint = _redisConnection.GetEndPoints().First();
            var keys = _redisConnection.GetServer(endpoint).Keys(pattern: _namespace+"*");

            return keys.Select(redisKey => database.StringGet(redisKey).ToString().Replace("[", "").Replace("]", ""))
                .Select(JsonConvert.DeserializeObject<T>).Where(x => !string.IsNullOrEmpty(x.ToString())).ToList();
        }

        public List<T> GetMultiple<T>(List<Guid> ids)
        {
            var database = _redisConnection.GetDatabase();
            List<RedisKey> keys = new List<RedisKey>();
            foreach (var id in ids)
            {
                keys.Add(MakeKey(id));
            }
            var serializedItems = database.StringGet(keys.ToArray(), CommandFlags.None);
            List<T> items = new List<T>();
            foreach (var item in serializedItems)
            {
                items.Add(JsonConvert.DeserializeObject<T>(item.ToString()));
            }
            return items;
        }

        public bool Exists(Guid id)
        {
            return Exists(id.ToString());
        }

        public bool Exists(string keySuffix)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            var serializedObject = database.StringGet(key);
            return !serializedObject.IsNullOrEmpty;
        }

        public void Save(Guid id, object entity)
        {
            Save(id.ToString(), entity);
        }

        public void Save(string keySuffix, object entity)
        {
            var key = MakeKey(keySuffix);
            var database = _redisConnection.GetDatabase();
            database.StringSet(MakeKey(key), JsonConvert.SerializeObject(entity));
        }

        private string MakeKey(Guid id)
        {
            return MakeKey(id.ToString());
        }

        private string MakeKey(string keySuffix)
        {
            if (!keySuffix.StartsWith(_namespace + ":"))
            {
                return _namespace + ":" + keySuffix;
            }
            else return keySuffix; //Key is already suffixed with namespace
        }
    }
}
