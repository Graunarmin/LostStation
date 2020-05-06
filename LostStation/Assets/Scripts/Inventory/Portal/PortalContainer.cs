using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalContainer : MonoBehaviour, IItemContainer
{
    //How many ports does the container have?
    [SerializeField] int space = 4;

    //use array so the item appears in the slot we dragged it onto
    private Item[] aliens = new Item[4];
    public List<Item> insertedAliens = new List<Item>();

    void Start()
    {
        //aliens = new Item[4];
    }

    public bool AddItem(Item item, int index)
    {
        if (aliens[index] != null || IsFull())
        {
            Debug.Log("Not enough room here");
            return false;
        }
        insertedAliens.Add(item);
        aliens[index] = item;
        return true;
    }

    public bool ContainsItem(ItemAsset item)
    {
        foreach (Item alien in aliens)
        {
            if (alien.itemInfo == item)
            {
                return true;
            }
        }
        return false;
    }

    public bool ContainsItem(Item item)
    {
        foreach (Item alien in aliens)
        {
            if (alien == item)
            {
                return true;
            }
        }
        return false;
    }

    public Item GetItemAtIndex(int index)
    {
        return aliens[index];
    }

    public int GetIndex(ItemAsset item)
    {
        for (int i = 0; i < space; i++)
        {
            if (aliens[i].itemInfo == item)
            {
                return i;
            }
        }
        return -1;
    }

    public int GetIndex(Item item)
    {
        for (int i = 0; i < space; i++)
        {
            if (aliens[i] == item)
            {
                return i;
            }
        }
        return -1;
    }

    public bool IsFull()
    {
        //sobald ein Platz frei ist, ist der Container nicht voll
        foreach(Item alien in aliens)
        {
            if(alien == null)
            {
                return false;
            }
        }
        return true;
    }

    public bool RemoveItem(Item item)
    {
        for (int i = 0; i < space; i++)
        {
            if(aliens[i] == item)
            {
                insertedAliens.Remove(item);
                aliens[i] = null;
                return true;
            }
        }
        return false;
    }

    public bool RemoveItem(ItemAsset item)
    {
        for (int i = 0; i < space; i++)
        {
            if (aliens[i].itemInfo == item)
            {
                aliens[i] = null;
                insertedAliens.Remove(aliens[i]);
                return true;
            }
        }
        return false;
    }

    //returns the number of inserted Aliens,
    //NOT the size of the array (which is always 4)!
    public int Size()
    {
        Debug.Log("Inserted Aliens: " + insertedAliens.Count);
        return insertedAliens.Count;
    }

    public void SetSpace(int spaces)
    {
        aliens = new Item[spaces];
    }

    public List<Item> GetContainer()
    {
        return insertedAliens;
    }
}
