using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPrereq : Prerequisite
{
    public Collector requiredCollector;

    //check if Prerequisite is met
    public override bool Complete
    {
        get{
            if(InventoryManager.invManager.items.Count > 0)
            {
                if (InventoryManager.invManager.items.Contains(requiredCollector.collectableItem))
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
