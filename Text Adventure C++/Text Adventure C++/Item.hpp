#pragma once
#include <string>
#include <iostream>

class Item {
public:
    Item(const std::string& name, const std::string& description);

    void Inspect() const;
    std::string GetName() const;

private:
    std::string name;
    std::string description;
};

