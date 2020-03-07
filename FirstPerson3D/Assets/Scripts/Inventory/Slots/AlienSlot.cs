using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class AlienSlot : ItemSlot
{
    public string slotName;
    public override bool CanReceiveItem(Item receivedItem)
    {
        if (item == null && receivedItem is Alien)
        {
            return true;
        }
        return false;
    }

    //Not generic enough to put it in InventoryEventHandler
    //If user clicks on the slot: Test if all conditions are met
    public void ShowControls(BaseEventData data)
    {
        Debug.Log("There was a Click!");
        PointerEventData pointerData = data as PointerEventData;

        if (pointerData.button == PointerEventData.InputButton.Left && item == null)
        {
            //open up Canvas with controls
            PortalManager.portal.OpenControls();
        }
    }

    public void ShowControls()
    {
        Debug.Log("There was a Button Click!");
        if (item == null)
        {
            //open up Canvas with controls
            PortalManager.portal.OpenControls();
        }
    }

}
