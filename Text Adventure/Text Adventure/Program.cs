using System;
using System.Collections.Generic;
using TextAdventure;

namespace TextAdventure
{
   
    public interface IInspectable //Interface for inspecting items
    {
        void Inspect();
        string GetName();
    }
}

    class Program
    {
        static void Main(string[] args)
        {
            Room MasterBedroom = new Room("Master Bedroom", "Bedroom, Wow, it is cozy in this Bedroom :) ");
            Room Bathroom = new Room("Bathroom", "Bathroom, Holy crap! Anybody got some Febreze?");
            Room LivingRoom = new Room("Living Room", "A immensely large and interesting living space that the fellow members of your humble abode use on a daily basis.");
            Room BasementDoor = new Room("Basement Door", "A large wooden door that leads to the basement..." );
            Room Basement = new Room("Basement", "A large, dark and mysterious Basement.");

            Item Plunger = new Item("Plunger", "Used for... well... moving on. Perfect for sh*tty situations. P.S. I apologize for that joke.");
            Item Guitar = new Item("Guitar", "An old 6-string electric guitar that you promised you'd learn how to play, but never did...");
            Item DirtySock = new Item("Dirty Sock", "An old sock, eww gross");
            Item Mirror = new Item("Mirror", "A dirty mirror, oh hey! You can see your ugly reflection!");
            Item Key = new Item("Key", "A key that can be used to open something...");
            Item MonsterHide = new Item("Monster Hide", "Hide of an unknown beast, could be useful... ");

            MasterBedroom.AddExit("north", Bathroom);  
            Bathroom.AddExit("south", MasterBedroom);
            Bathroom.AddExit("east", LivingRoom); 

            LivingRoom.AddExit("west", MasterBedroom);  
            LivingRoom.AddExit("north", Bathroom);  
            LivingRoom.AddExit("south", BasementDoor);

            BasementDoor.AddExit("south", Basement);
            BasementDoor.AddExit("north", LivingRoom);

            Basement.AddExit("north", BasementDoor);

            Bathroom.AddInspectable(Plunger);
            LivingRoom.AddInspectable(Mirror);
            MasterBedroom.AddInspectable(Guitar);
            MasterBedroom.AddInspectable(DirtySock);
            Bathroom.AddInspectable(Key);
            Basement.AddInspectable(MonsterHide);


            Game game = new Game(MasterBedroom);  //Start game in MasterBedroom
            game.Start();
        }
    }

