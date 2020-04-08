using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour
{
    protected Item item;
    public Image icon;
    //public Button removeButton;

    public void AddItemToSlot(Item newItem)
    {
        item = newItem;

        icon.sprite = item.itemInfo.icon;
        icon.enabled = true;
        if(!GameManager.gameManager.PortalPuzzle() && !GameManager.gameManager.Crafting())
        {
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.openInventory);
        }
        //removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        //removeButton.interactable = true;
    }

    public virtual void Hover()
    {
        
    }

    public virtual void HoverExit()
    {

    }

    public virtual void BeginDrag(BaseEventData data)
    {
        InventoryEventHandler.inventoryEvents.BeginDrag(this, data);
    }

    public virtual void Drag(BaseEventData data)
    {
        InventoryEventHandler.inventoryEvents.Drag(this, data);
    }

    public virtual void EndDrag()
    {
        InventoryEventHandler.inventoryEvents.EndDrag(this);
    }

    public virtual void Drop()
    {
        InventoryEventHandler.inventoryEvents.Drop(this);
    }


    public Item GetItem()
    {
        return item;
    }

    public virtual bool CanReceiveItem(Item receivedItem)
    {
        //only enable drag and drop between inventory and crafting?
        if(item == null)
        {
            return true;
        }
        return false;
    }

    public virtual bool IsEmpty()
    {
        return item == null;
    }
}
