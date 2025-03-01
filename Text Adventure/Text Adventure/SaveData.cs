using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace TextAdventure
{
    public static class SaveData
    {
        private static readonly string SaveFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "GameSave.txt"); 
        //I could not for the life of me get the file path to work if I put it next to the exe for some reason..
        //Had to figure out an alternitive way by saving in my docs folder
        public static void SaveGame(Game game)
        {
            try
            {
                if (game.CurrentRoom == null)
                {
                    Console.WriteLine("Error: No current room to save.");
                    return;
                }

                var saveData = new GameSaveData
                {
                    PlayerInventory = game.PlayerInventory._items.ToList(),
                    CurrentRoomName = game.CurrentRoom.GetName(),
                    RoomInventories = game.AllRooms.ToDictionary(
                        room => room.Key,
                        room => room.Value.Inspectables.OfType<Item>().ToList()
                    )
                };

                string json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions { WriteIndented = true });
                //Pretty printing :)
                File.WriteAllText(SaveFilePath, json);
                Console.WriteLine($"Game saved successfully to: {SaveFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save game: {ex.Message}");
            }
        }

        public static void LoadGame(Game game)
        {
            try
            {
                if (!File.Exists(SaveFilePath))
                {
                    Console.WriteLine("No save file found.");
                    return;
                }

                string json = File.ReadAllText(SaveFilePath);
                var saveData = JsonSerializer.Deserialize<GameSaveData>(json);

                if (saveData == null)
                {
                    Console.WriteLine("Error loading save file.");
                    return;
                }

                game.PlayerInventory = new Inventory();
                foreach (var item in saveData.PlayerInventory)
                {
                    game.PlayerInventory.AddItem(item);
                }

                if (saveData.CurrentRoomName != null && game.AllRooms.TryGetValue(saveData.CurrentRoomName, out var loadedRoom))
                {
                    game.CurrentRoom = loadedRoom;
                }
                else
                {
                    Console.WriteLine("Warning: Saved room not found. Starting in default room.");
                }

                foreach (var room in game.AllRooms)
                {
                    if (saveData.RoomInventories.TryGetValue(room.Key, out var items))
                    {
                        room.Value.LoadRoomInventory(items);
                    }
                }

                Console.WriteLine("Game loaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load game: {ex.Message}");
            }
        }
    }

    public class GameSaveData
    {
        public List<Item> PlayerInventory { get; set; } = new List<Item>();
        public string CurrentRoomName { get; set; }
        public Dictionary<string, List<Item>> RoomInventories { get; set; } = new Dictionary<string, List<Item>>();
    }
}
