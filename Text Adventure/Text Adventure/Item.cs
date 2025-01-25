using System;
namespace TextAdventure
{
   
public class Item : IInspectable
{
    public string Name { get; }
    public string Description { get; }

    public Item(string name, string description)
    {
        Name = name;
        Description = description;
    }

    // Show the full description when inspecting an item
    public void Inspect()
    {
        Console.WriteLine(Description);
    }

    // Return only the name of the item for 'look' command
    public string GetName()
    {
        return Name;  // Return only the name of the item
    }
}

}

    
