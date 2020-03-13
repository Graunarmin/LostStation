using System.Collections.Generic;

public interface IItemContainer
{
    bool AddItem(Item item, int index = 0);
    bool RemoveItem(Item item);
    bool RemoveItem(ItemAsset item);
    Item GetItemAtIndex(int index);
    bool ContainsItem(ItemAsset item);
    bool ContainsItem(Item item);
    bool IsFull();
    int Size();
    void SetSpace(int spaces);
    List<Item> GetContainer();
}
