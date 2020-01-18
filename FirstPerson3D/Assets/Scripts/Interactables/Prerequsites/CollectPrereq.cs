using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPrereq : Prerequisite
{

    //if true check for collectable instead of switcher
    //public bool requireCollectable;

    //if requireCollectable is true, check this collector
    public Collector requiredCollector;

    //check if Prerequisite is met
    public override bool Complete
    {
        get{
            if(Reference.instance.collectHeld != null)
            {
                return Reference.instance.collectHeld.collectName
                    == requiredCollector.collectableItem.collectName;
            }
            else
            {
                return false;
            }
            
        }
    }

    public override void Print()
    {
        Debug.Log("Collect Prerequisite");
    }
}
