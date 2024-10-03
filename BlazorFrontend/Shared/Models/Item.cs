namespace BlazorFrontend.Shared.Models
{ 
    public class Item
    {
        public string Identifier { get; set; }
        public string Name { get; set; } // Change from int to string
        public int Amount { get; set; }

        public Item(string identifier, string name, int amount)
        {
            Identifier = identifier;
            Name = name;
            Amount = amount;
        }
    }    
}


