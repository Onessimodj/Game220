using System;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace TextAdventure
{
    public enum GameState
    {
        PlayingGame,
        Exiting,
    }

    public class Game
    {
        public IInspectable _currentInspectable;
        public GameState _currentState;
        private Inventory _playerInventory;
        public Room CurrentRoom { get; set; }
        public Dictionary<string, Room> AllRooms { get; set; }

        public Inventory PlayerInventory 
        { 
            get { return _playerInventory; } 
            set { _playerInventory = value; }
        }

       public Game(Room startingRoom)
        {
         CurrentRoom = startingRoom ?? throw new ArgumentNullException(nameof(startingRoom)); //Chat cpt helped me figure out an solution to an exception and used nullable types here to avoid crashing/exception
         _currentInspectable = startingRoom; 
         _currentState = GameState.PlayingGame;
         _playerInventory = new Inventory();
         AllRooms = new Dictionary<string, Room>();
        }



        public void Start()
        {
            string command;

            while (_currentState == GameState.PlayingGame)
            {
                Console.WriteLine("Do you want to 'look', 'inspect', 'move', 'take', 'drop', 'inventory', 'load save', 'save', or 'exit game'?");
                command = Console.ReadLine().ToLower().Trim();

                switch (command)
                {
                    case "look":
                        _currentInspectable.Inspect();
                        break;

                    case "inspect":
                        Console.WriteLine("What are you trying to inspect?");
                        break;

                    case "save":
                        SaveData.SaveGame(this);
                        break;

                    case "load save":
                        SaveData.LoadGame(this);
                        break;

                    case var cmd when cmd.StartsWith("inspect"):
                        string itemName = command.Substring(7).Trim();
                        InspectItem(itemName);
                        break;

                    case var cmd when cmd.StartsWith("unlock door"):
                        UseKey();
                        break;

                    case var cmd when cmd.StartsWith("open door"):
                        UseKey();
                        break;

                    case "exit game":
                        ConfirmExit();
                        break;

                    case "move":
                        Console.WriteLine("I need more info please, where are you trying to move to?");
                        break;

                    case "take":
                        Console.WriteLine("What do you want to take??? I need more info :(");
                        break;

                    case "drop":
                        Console.WriteLine("What are you trying to drop??? I need more info :(");
                        break;

                    case var cmd when cmd.StartsWith("move"):
                        Move(command.Substring(5).Trim());
                        break;

                    case var cmd when cmd.StartsWith("take"):
                        TakeItem(command.Substring(5).Trim());
                        break;

                    case var cmd when cmd.StartsWith("drop"):
                        DropItem(command.Substring(5).Trim());
                        break;

                    case "inventory":
                        _playerInventory.ShowInventory();
                        break;

                    default:
                        Console.WriteLine("Mr. Kyle didn't teach me that yet... try typing a valid command :)");
                        break;
                }
            }
        }

        private void TakeItem(string itemName)
        {
            if (_currentInspectable is Room room)
            {
                var item = room.Inspectables.OfType<Item>().FirstOrDefault(i => i.GetName().ToLower() == itemName.ToLower());

                if (item != null)
                {
                    if (_playerInventory.AddItem(item))
                    {
                        room.Inspectables.Remove(item);
                        Console.WriteLine($"You took the {itemName}.");
                    }
                    else
                    {
                        Console.WriteLine("Your pockets are full!!! Your inventory only has 10 slots...");
                    }
                }
                else
                {
                    Console.WriteLine($"There is no item named '{itemName}' here.");
                }
            }
        }

        private void DropItem(string itemName)
        {
            var item = _playerInventory._items.FirstOrDefault(i => i.GetName().ToLower() == itemName.ToLower());

            if (item != null)
            {
                _playerInventory.RemoveItem(item);

                if (_currentInspectable is Room room)
                {
                    room.AddInspectable(item);
                    Console.WriteLine($"You have dropped the {itemName}.");
                }
            }
            else
            {
                Console.WriteLine($"You don't have an item named '{itemName}' in your inventory.");
            }
        }

        private void UseKey()
        {
            if (_playerInventory.HasItem("Key"))
            {
                if (_currentInspectable is Room room && room.Name == "Basement Door")
                {
                    if (room.Exits.ContainsKey("south"))
                    {
                        var basementDoor = room.Exits["south"];

                        if (basementDoor.IsLocked)
                        {
                            basementDoor.Unlock();
                            Console.WriteLine("You used the key to unlock the basement door.");
                            StartBattle();
                        }
                        else
                        {
                            Console.WriteLine("The basement door is already unlocked.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("There is no basement door here.");
                    }
                }
                else
                {
                    Console.WriteLine("There is nothing to unlock here.");
                }
            }
            else
            {
                Console.WriteLine("You don't have the key to unlock the door.");
            }
        }

        private void ConfirmExit()
        {
            Console.WriteLine("Are you sure you want to exit? (y/n)");
            string response = Console.ReadLine().ToLower().Trim();

            if (response == "y" || response == "yes")
            {
                _currentState = GameState.Exiting;
                Console.WriteLine("Ok bye! Thanks for playing :)");
            }
            else
            {
                Console.WriteLine("I guess you changed your mind... let's continue");
            }
        }

        private void Move(string direction)
        {
            if (_currentInspectable is Room room)
            {
                if (room.Name == "Basement Door" && direction == "south" && room.Exits.ContainsKey(direction))
                {
                    var basementDoor = room.Exits[direction];
                    if (basementDoor.IsLocked)
                    {
                        Console.WriteLine("The basement door is locked! You can't go in.");
                        return;
                    }
                }

                if (room.Exits.ContainsKey(direction))
                {
                    _currentInspectable = room.Exits[direction];
                    Console.WriteLine($"You move {direction}.");
                }
                else
                {
                    Console.WriteLine("That is not a valid direction.");
                }
            }
        }

        public Room FindRoomByName(string roomName)
        {
            return AllRooms.Values.FirstOrDefault(room => room.Name.Equals(roomName, StringComparison.OrdinalIgnoreCase));
        }

        private void StartBattle()
        {
            string playerName = "Kyle";
            Duel battle = new Duel(playerName);
            battle.StartBattle();
        }

        private void InspectItem(string itemName)
        {
            if (_currentInspectable is Room room)
            {
                var item = room.Inspectables.OfType<Item>().FirstOrDefault(i => i.Name.ToLower() == itemName.ToLower());
                if (item != null)
                {
                    item.Inspect();
                }
                else
                {
                    Console.WriteLine($"There is no item named '{itemName}' here.");
                }
            }
            else
            {
                Console.WriteLine("There is nothing to inspect.");
            }
        }
    }
}
