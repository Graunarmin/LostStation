using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienCollector : Collector
{
    public override void PickUpItem()
    {
        //pick it up, look at it and put it in the backpack
        base.PickUpItem();

        //and add it to the collected aliens
        AlienManager.mrSaru.AddAlien(GetComponent<Alien>());
    }
}
