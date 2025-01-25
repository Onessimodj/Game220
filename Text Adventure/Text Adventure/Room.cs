using System;
namespace TextAdventure
{

public class Room : IInspectable //Room class inherites from interface
{
    public string Name { get; }
    public string Description { get; }
    public Dictionary<string, Room> Exits { get; } //Dictionary 
    public List<IInspectable> Inspectables { get; } //List

    public Room(string name, string description)
    {
        Name = name;
        Description = description;
        Exits = new Dictionary<string, Room>();
        Inspectables = new List<IInspectable>();
    }

    
    public void AddExit(string direction, Room room)
    {
        Exits[direction] = room;
    }

    public void AddInspectable(IInspectable inspectable)
    {
        Inspectables.Add(inspectable);
    }

    public void Inspect()
    {
        Console.WriteLine(Description);
        Console.WriteLine($"Exits: {string.Join(", ", Exits.Keys)}");
        
        if (Inspectables.Count > 0)
        {
            Console.WriteLine("You can inspect the following:");
            foreach (var inspectable in Inspectables)
            {
                Console.WriteLine($"- {inspectable.GetName()}"); 
            }
        }
    }
    public string GetName()
    {
        return Name; 
    }
}

}