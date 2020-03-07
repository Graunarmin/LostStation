using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSlot : ItemSlot
{
    public override bool CanReceiveItem(Item receivedItem)
    {
        if (item == null && receivedItem is Alien)
        {
            return true;
        }
        return false;
    }
}
