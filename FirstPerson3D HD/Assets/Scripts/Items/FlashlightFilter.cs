using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightFilter : Item
{
    public void Equip()
    {
        Debug.Log("Equipping the Filter!");
        //Remove Filter from inventory
        InventoryManager.invManager.RemoveItem(this);
    }
}
