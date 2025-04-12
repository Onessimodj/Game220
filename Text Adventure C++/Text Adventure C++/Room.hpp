#pragma once
#include <string>
#include <map> //Dictionary
#include <vector>//List

//Just so you know I had to resist the urge to use "using namespace std" here, I know you said not to use that in header files but man this syntax is so ugly :(

class Room {
public:
    Room(const std::string& name, const std::string& description);

    void AddExit(const std::string& direction, Room* room);
    void AddInspectable(const std::string& itemName);
    void Inspect() const;

    Room* GetExit(const std::string& direction) const;
    const std::vector<std::string>& GetInspectables() const;  

private:
    std::string name;
    std::string description;
    std::map<std::string, Room*> exits;
    std::vector<std::string> inspectables;
};

