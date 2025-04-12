#include <iostream>
#include "Room.hpp"
#include "Item.hpp"
#include "Input.hpp"


int main() {
    Room* livingRoom = new Room("Living Room", "A immensely large and interesting living space that the fellow members of your humble abode use on a daily basis.");
    Room* kitchen = new Room("Kitchen", "A welcoming room typically used to get/make food!");

    livingRoom->AddExit("north", kitchen);
    kitchen->AddExit("south", livingRoom);

    Item* key = new Item("Key", "A shiny key.");
    livingRoom->AddInspectable("Key");

    Game game(livingRoom);
    game.Start();

    delete key;
    delete livingRoom; //Used new on rooms must delete them!!!
    delete kitchen;

    return 0;
}
