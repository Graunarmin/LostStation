using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(Item))]
public class Collector : Interactable
{
    //public string infoText;

    [HideInInspector]
    public Collectable collectableItem;
    //to control if the item can be collected atm
    //bool collectablePresent;
    private void Awake()
    {
        collectableItem = GetComponent<Collectable>();
    }


    public override void Interact()
    {
        //on click we collect the item
        Reference.instance.collectHeld = collectableItem;
        Reference.instance.inventoryDisplay.Activate();
        //Hide the Object Information
        HideInfo();
        //and set it to inactive so it is no longer visible or accessible
        gameObject.SetActive(false);
    }
}
