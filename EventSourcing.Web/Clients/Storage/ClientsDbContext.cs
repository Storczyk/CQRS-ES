using System;
using System.Collections.Generic;
using System.Linq;
using EventSourcing.Web.ClientsContracts.Events;
using EventSourcing.Web.Domain.Events;
using EventSourcing.Web.TransactionsContracts.Accounts.Events;
using Newtonsoft.Json;
using ReflectionMagic;
using StackExchange.Redis;
using StackExchange.Redis.KeyspaceIsolation;

namespace EventSourcing.Web.Clients.Storage
{
    public abstract class ClientsDbContext
    {
        private readonly IConnectionMultiplexer _redisConnection;

        public ClientsDbContext(IConnectionMultiplexer redis)
        {
            _redisConnection = redis;
        }

        public List<IEvent> GetEvents(Guid id)
        {
            return GetEvents(id.ToString());
        }

        public List<IEvent> GetEvents(string keySuffix)
        {
            var key = MakeKey(Guid.NewGuid().ToString(), Guid.Empty);
            var database = _redisConnection.GetDatabase();
            var endpoint = _redisConnection.GetEndPoints().First();
            var keys = _redisConnection.GetServer(endpoint).Keys(pattern: "*" + key + "*");
            var events = new List<IEvent>();
            foreach (var redisKey in keys)
            {
                var obj = database.StringGet(key);
                if (obj.IsNullOrEmpty) throw new ArgumentNullException();
                var json = JsonConvert.DeserializeObject<IEvent>(obj.ToString());
                switch (json.EventType)
                {
                    case EventType.ClientCreated:
                        events.Add(JsonConvert.DeserializeObject<ClientCreatedEvent>(obj.ToString()));
                        break;
                    case EventType.ClientUpdated:
                        events.Add(JsonConvert.DeserializeObject<ClientUpdatedEvent>(obj.ToString()));
                        break;
                    case EventType.AccountCreated:
                        events.Add(JsonConvert.DeserializeObject<NewAccountCreatedEvent>(obj.ToString()));
                        break;
                }
            }
            

            return events;
        }

        public List<T> GetAll<T>()
        {
            var database = _redisConnection.GetDatabase();
            var endpoint = _redisConnection.GetEndPoints().First();
            var keys = _redisConnection.GetServer(endpoint).Keys(pattern: "*");

            foreach (var redisKey in keys)
            {
                var x = database.StringGet(redisKey);
            }
            return new List<T>();
        }

        public List<T> Load<T>() where T : class
        {
            var database = _redisConnection.GetDatabase();
            var endpoint = _redisConnection.GetEndPoints().First();
            var keys = _redisConnection.GetServer(endpoint).Keys(pattern: "*");
            var list = new List<T>();
            foreach (var redisKey in keys)
            {
                var obj = database.StringGet(redisKey);
                var deserializedObject = JsonConvert.DeserializeObject<T>(obj);
                if (deserializedObject != null)
                {
                    list.Add(deserializedObject);
                }
            }

            return list;
        }

        public void Save<T>(T entity)
        {
            var database = _redisConnection.GetDatabase();
            database.StringSet(entity.AsDynamic().AggregateId.ToString(), JsonConvert.SerializeObject(entity));
        }

        public void Save(Guid aggregateId, IEvent @event)
        {
            Save(aggregateId.ToString(), @event);
        }

        public void Save(string aggregateId, IEvent @event)
        {
            var key = MakeKey(aggregateId, Guid.NewGuid());
            var database = _redisConnection.GetDatabase();
            database.StringSet(key, JsonConvert.SerializeObject(@event));
        }

        private string MakeKey(string aggregateId, Guid id)
        {
            if (id == Guid.Empty)
                return aggregateId;
            else
            {
                return aggregateId + ":" + id;
            }
        }
    }
}
