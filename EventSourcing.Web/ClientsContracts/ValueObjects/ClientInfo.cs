namespace EventSourcing.Web.ClientsContracts.ValueObjects
{
    public class ClientInfo
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public ClientInfo(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}