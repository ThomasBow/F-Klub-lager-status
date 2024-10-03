


namespace WebServer.Requests {
    public class ItemRequest
    {
        public string Identifier { get; set; }
        public string Name { get; set; }

        public ItemRequest(string identifier, string name)
        {
            Identifier = identifier;
            Name = name;
        }
    }
}