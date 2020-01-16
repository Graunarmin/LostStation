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
        //Reference.instance.inventoryDisplay.Activate();
        //Hide the Object Information
        HideInfo();
        InspectObject();
        //and set it to inactive so it is no longer visible or accessible
        gameObject.SetActive(false);
    }

    public void InspectObject()
    {
        //"duplicate" the clicked on Object
        GameObject item = Instantiate(gameObject);

        //set the rig as the parent (as in the unity history)
        item.transform.SetParent(Reference.instance.obsCam.rig);

        //bring it to the middle of the observer Camera's Screen
        item.transform.localPosition = Vector3.zero;

        //push the model of the Prop down so the pivot is aligned with the "actual" Prop
        item.transform.GetChild(0).localPosition = Vector3.zero;

        Reference.instance.obsCam.model = item.transform;

        //turn on observer Camera
        Reference.instance.obsCam.Activate();
    }
}
