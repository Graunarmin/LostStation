using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSlot : ItemSlot
{
    public override bool CanReceiveItem(Item receivedItem)
    {
        if (item == null && receivedItem is Alien)
        {
            Debug.Log(name + " can receive the " + receivedItem.name + " item");
            return true;
        }
        Debug.Log(name + " can't receive the " + receivedItem.name + " item");
        return false;
    }
}
