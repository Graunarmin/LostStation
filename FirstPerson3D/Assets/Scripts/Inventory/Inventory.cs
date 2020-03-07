using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour, IItemContainer
{
    [SerializeField] int space;
    
    private List<Item> items = new List<Item>();

    public bool AddItem(Item item, int index = 0)
    {
        //Check if this was the first collectable and if so
        //show info on how to access inventory
        TutorialManager.tutorialManager.FirstCollectable();

        if (IsFull())
        {
            Debug.Log("Not enough room");
            return false;
        }
        items.Add(item);
        return true;
    }

    
    public bool RemoveItem(Item item)
    {
        items.Remove(item);
        return true;
    }

    public bool RemoveItem(ItemAsset item)
    {
        foreach (Item containedItem in items)
        {
            if (containedItem.itemInfo == item)
            {
                items.Remove(containedItem);
                return true;
            }
        }
        return false;
    }

    public Item GetItemAtIndex(int index)
    {
        return items[index];
    }

    public bool ContainsItem(Item item)
    {
        if(items.Contains(item))
        {
            Debug.Log("Inventory contains " + item.name);
            return true;
        }
        else
        {
            Debug.Log("Inventory does not contain " + item.name);
        }
        return false;
    }

    public bool ContainsItem(ItemAsset item)
    {
        foreach(Item containedItem in items)
        {
            if(containedItem.itemInfo == item)
            {
                Debug.Log("Inventory contains " + item.name);
                return true;
            }
        }
        Debug.Log("Inventory does not contain " + item.name);
        return false;
    }

    public bool IsFull()
    {
        if(items.Count >= space)
        {
            return true;
        }
        return false;
    }

    public int Size()
    {
        return items.Count;
    }

    public void SetSpace(int spaces)
    {
        space = spaces;
    }

}
