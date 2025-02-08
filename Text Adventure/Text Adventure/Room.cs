using System;
using System.Collections.Generic;

namespace TextAdventure
{
    public class Room : IInspectable // Room class inherits from IInspectable
    {
        public string Name { get; }
        public string Description { get; }
        public Dictionary<string, Room> Exits { get; } // Dictionary for room exits
        public List<IInspectable> Inspectables { get; } // List of items/inspectables in the room

        public Room(string name, string description) // Constructor for Room
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
            Console.WriteLine($"Exits: {string.Join(", ", Exits.Keys)}"); //exits

            if (Inspectables.Count > 0)
            {
                Console.WriteLine("You can inspect and take the following:");
                foreach (var inspectable in Inspectables)
                {
                    Console.WriteLine($"- {inspectable.GetName()}"); // List inspectable items
                }
            }
        }

        public string GetName()
        {
            return Name;
        }
    }
}
