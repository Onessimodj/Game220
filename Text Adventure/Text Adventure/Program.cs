using System;
using System.Collections.Generic;

namespace TextAdventure
{
    public interface IInspectable
    {
        void Inspect();
        string GetName();
    }
    class Program
    {
        static void Main(string[] args)
        {
           
            Room MasterBedroom = new Room("Master Bedroom", "Bedroom, Wow, it is cozy in this Bedroom :) ");
            Room Bathroom = new Room("Bathroom", "Bathroom, Holy crap! Anybody got some Febreze?");

           
            Item Plunger = new Item("Plunger", "Used for... well... moving on. Perfect for sh*tty situations. P.S. I apologize for that joke.");
            Item Guitar = new Item("Guitar", "An old 6-string electric guitar that you promised you'd learn how to play, but never did...");

           
            MasterBedroom.AddExit("north", Bathroom);
            Bathroom.AddExit("south", MasterBedroom);

           
            Bathroom.AddInspectable(Plunger);
            MasterBedroom.AddInspectable(Guitar);

           
            Game game = new Game(MasterBedroom);  
            game.Start();  
        }
    }
}
