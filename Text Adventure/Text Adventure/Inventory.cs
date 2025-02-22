namespace TextAdventure
{
    public class Inventory
{
    public List<Item> _items;
    private const int MaxItems = 10; //conts keyword means it cannot be changed

    public Inventory()
    {
        _items = new List<Item>();
    }

    public bool AddItem(Item item)
    {
        if (_items.Count < MaxItems)
        {
            _items.Add(item);
            return true;
        }
        return false;
    }

    public bool RemoveItem(Item item)
    {
        return _items.Remove(item);
    }

    public bool HasItem(string itemName) //bool to check if player has item
    {
        return _items.Any(i => i.GetName().ToLower() == itemName.ToLower());
    }

    public void ShowInventory()
    {
        if (_items.Count == 0)
        {
            Console.WriteLine("There's nothing in here!!!");
        }
        else
        {
            Console.WriteLine("Your inventory contains:");
            foreach (var item in _items)
            {
                Console.WriteLine($"- {item.GetName()}");
            }
        }
    }
}

}