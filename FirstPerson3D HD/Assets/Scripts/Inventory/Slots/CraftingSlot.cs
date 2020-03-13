using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingSlot : ItemSlot
{
    public override bool CanReceiveItem(Item receivedItem)
    {
        if(receivedItem is Alien)
        {
            return false;
        }
        if (item == null)
        {
            return true;
        }
        return false;
    }
}
