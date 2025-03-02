using System;
using System.Collections.Generic;

namespace TextAdventure
{
    public class Room : IInspectable
    {
        public string Name { get; }
        public string Description { get; }
        public Dictionary<string, Room> Exits { get; }
        public List<IInspectable> Inspectables { get; } = new List<IInspectable>();
        public bool IsLocked { get; private set; }

        public Room(string name, string description, bool isLocked = true)
        {
            Name = name;
            Description = description;
            Exits = new Dictionary<string, Room>();
            IsLocked = isLocked;
        }

        public void AddExit(string direction, Room room)
        {
            Exits[direction] = room;
        }

        public void AddInspectable(IInspectable inspectable)
        {
            Inspectables.Add(inspectable);
        }

        public void Unlock()
        {
            IsLocked = false;
            Console.WriteLine($"{Name} has been unlocked!");
        }
        public void LoadRoomInventory(List<Item> items)
        {
         Inspectables.Clear();
         foreach (var item in items)
         {
              AddInspectable(item);
         }
        }


        public void Inspect()
        {
            Console.WriteLine($"{Name} - {Description}");

            if (Inspectables.Count > 0)
            {
                Console.WriteLine("You see:");
                foreach (var item in Inspectables)
                {
                    Console.WriteLine($"- {item.GetName()}");
                }
            }

            if (Exits.Count > 0)
            {
                Console.WriteLine("Exits:");
                foreach (var exit in Exits.Keys)
                {
                    Console.WriteLine($"- {exit}");
                }
            }
        }

        public string GetName()
        {
            return Name;
        }
    }
}
