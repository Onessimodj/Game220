#include "Input.hpp"
#include <iostream>
#include <algorithm> //Searched online for a way to use to lower!!!
using namespace std;


Game::Game(Room* startingRoom)
    : currentRoom(startingRoom), currentState(GameState::PlayingGame) {}

void Game::Start() {
    string command;
    while (currentState == GameState::PlayingGame) {
        cout << "Do you want to 'look', 'inspect', 'move', or 'exit'?\n";
        getline(cin, command);
        transform(command.begin(), command.end(), command.begin(), ::tolower); // Converts command to lowercase

        if (command == "look") {
            currentRoom->Inspect();
        }
        else if (command == "inspect") {
            InspectItem();
        }
        else if (command == "move") {
            Move();
        }
        else if (command == "exit") {
            ConfirmExit();
        }
        else {
            cout << "Huh?\n";
        }
    }
}

void Game::InspectItem() {
    cout << "What do you want to inspect?\n";
    string itemName;
    getline(cin, itemName);

    bool itemFound = false;
    for (const string& item : currentRoom->GetInspectables()) {
        if (item == itemName) {
            cout << "You inspect the " << item << ".\n"; //need to figure out a way to properly display item description, working on updating this...
            itemFound = true;
            break;
        }
    }

    if (!itemFound) { //If it couldn't find item
        cout << "There is no '" << itemName << "' here.\n";
    }
}

void Game::Move() {
    cout << "What direction do you want to move?\n";
    string direction;
    getline(cin, direction);

    Room* nextRoom = currentRoom->GetExit(direction);
    if (nextRoom) {
        currentRoom = nextRoom;
        cout << "You move " << direction << ".\n";
        currentRoom->Inspect();
    } else {
        cout << "You can't go that way.\n";
    }
}

void Game::ConfirmExit() {
    cout << "Are you sure you want to exit? (y/n)\n";
    string response;
    getline(cin, response);

    if (response == "y" || response == "yes") {
        currentState = GameState::Exiting;
        cout << "Thanks for playing! Goodbye. :) \n";
    } else {
        cout << "Moving along...\n";
    }
}

