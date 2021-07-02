
public interface IItemContainer
{
    bool CanAddItem(Item item, int amount = 1);
    bool AddItem(Item item, int amount = 1);

    Item RemoveItem(string itemID, int amount = 1);
    bool RemoveItem(Item item, int amount = 1);

    void Clear();

    int ItemCount(string itemID);
}
