#pragma once
#include <string>
#include <map>
#include <vector>
#include "Room.hpp"
#include "Item.hpp"


enum class GameState {
    PlayingGame,
    Exiting
};

class Game {
public:
    Game(Room* startingRoom);

    void Start();  

private:
    Room* currentRoom;
    GameState currentState;

    void InspectItem();
    void Move();
    void ConfirmExit();
};



