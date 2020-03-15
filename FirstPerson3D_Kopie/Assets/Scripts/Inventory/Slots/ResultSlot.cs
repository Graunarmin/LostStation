using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultSlot : ItemSlot
{
    public override bool CanReceiveItem(Item receivedItem)
    {
        return false;
    }
}
