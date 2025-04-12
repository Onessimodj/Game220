using namespace std;
#include "Item.hpp"

Item::Item(const string& name, const string& description)
    : name(name), description(description) {}

void Item::Inspect() const {
    cout << description << endl;
}

string Item::GetName() const {
    return name;
}
