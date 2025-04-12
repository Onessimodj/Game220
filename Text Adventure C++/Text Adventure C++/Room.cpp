#include "Room.hpp"
#include <iostream>
using namespace std;

Room::Room(const string& name, const string& description)
    : name(name), description(description) {}

void Room::AddExit(const string& direction, Room* room) {
    exits[direction] = room;
}

void Room::AddInspectable(const string& itemName) {
    inspectables.push_back(itemName);
}

void Room::Inspect() const {
    cout << "You are in " << name << ". " << description << "\n";

    if (!inspectables.empty()) {
        cout << "You see: ";
        for (const auto& item : inspectables) {
            cout << item << " ";
        }
        cout << "\n";
    }

    if (!exits.empty()) {
        cout << "Exits: ";
        for (const auto& exit : exits) {
        cout << exit.first << " ";
        }
        cout << "\n";
    }
}

Room* Room::GetExit(const string& direction) const {
    auto it = exits.find(direction); //used auto like var in c#
    if (it != exits.end()) {
        return it->second;
    }
    return nullptr; //Return null pointer if no exit is found
}

const vector<string>& Room::GetInspectables() const {
    return inspectables; 
}

