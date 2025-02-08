using System;

namespace TextAdventure
{
    public class Item : IInspectable
    {
        public string Name { get; }
        public string Description { get; }

        public Item(string name, string description) //Constructor for Item
        {
            Name = name;
            Description = description;
        }

        public void Inspect()
        {
            Console.WriteLine(Description);
        }

        public string GetName()
        {
            return Name;
        }
    }
}
