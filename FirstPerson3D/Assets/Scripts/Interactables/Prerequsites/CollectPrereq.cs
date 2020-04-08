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
            if(InventoryManager.invManager.GetContainerSize() > 0)
            {
                if (InventoryManager.invManager.ContainerContainsItem(requiredCollector.collectableItem))
                {
                    Debug.Log("Required Item in inventory");
                    return true;
                }
                Debug.Log("Required Item NOT in inventory");
                return false;
            }
            Debug.Log("Required Item NOT in inventory");
            return false;
        }
    }
}
