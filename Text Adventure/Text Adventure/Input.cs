using System;
namespace TextAdventure
{
public class Game
{
    private IInspectable _currentInspectable;  
    public Game(IInspectable startingInspectable)
    {
        _currentInspectable = startingInspectable;
    }

    public void Start()
    {
        string command;
        while (true)
        {
            Console.WriteLine("Do you want to 'look', 'inspect', or 'move'?");
            command = Console.ReadLine().ToLower().Trim();

            if (command == "look")
            {
                _currentInspectable.Inspect(); 
            }
            else if (command.StartsWith("inspect"))
            {
                var itemName = command.Substring(8).Trim();
                InspectItem(itemName); 
            }
            else if (command.StartsWith("move"))
            {
                var direction = command.Substring(5).Trim();
                Move(direction); 
            }
            else
            {
                Console.WriteLine("Mr. Kyle didn't teach me that yet... try typing a command please :) ");
            }
        }
    }

    private void Move(string direction)
    {
        if (_currentInspectable is Room room && room.Exits.ContainsKey(direction))
        {
            _currentInspectable = room.Exits[direction];
            Console.WriteLine($"You move {direction}.");
        }
        else
        {
            Console.WriteLine("You can't go that way.");
        }
    }

    private void InspectItem(string itemName)
    {
        if (_currentInspectable is Room room)
        {
            var item = room.Inspectables.OfType<Item>().FirstOrDefault(i => i.Name.ToLower() == itemName);
            if (item != null)
            {
                item.Inspect(); 
            }
            else
            {
                Console.WriteLine($"There is no item named {itemName} here.");
            }
        }
        else
        {
            Console.WriteLine("There is nothing to inspect.");
        }
    }
}

}
       