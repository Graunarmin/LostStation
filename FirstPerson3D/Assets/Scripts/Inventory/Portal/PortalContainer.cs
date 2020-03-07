﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalContainer : MonoBehaviour, IItemContainer
{
    //How many ports does the container have?
    [SerializeField] int space = 4;

    //use array so the item appears in the slot we dragged it onto
    private Item[] aliens = new Item[4];

    void Start()
    {
        //aliens = new Item[4];
    }

    public bool AddItem(Item item, int index)
    {
        Debug.Log("Add at index " + index);
        if (aliens[index] != null)
        {
            Debug.Log("Not enough room here");
            return false;
        }
        if (IsFull())
        {
            Debug.Log("Is Full");
            return false;
        }
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
            //Debug.Log("Comparing " + aliens[i].name + " with " + item.name);
            if(aliens[i] == item)
            {
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
            //Debug.Log("Comparing " + aliens[i].name + " with " + item.name);
            if (aliens[i].itemInfo == item)
            {
                aliens[i] = null;
                return true;
            }
        }
        return false;
    }

    public int Size()
    {
        return space;
    }

    public void SetSpace(int spaces)
    {
        aliens = new Item[spaces];
    }
}
