using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : ItemContainerManager
{
    #region Singleton
    public static PortalManager portal;
    private void Awake()
    {

        if (portal == null)
        {
            portal = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of PortalManager!");
        }
        slots = new ItemSlot[] {null};
    }
    #endregion

    private AlienSlot activeSlot;
    [SerializeField] ItemSlot[] alienSlots;

    protected override void Start()
    {
        container = new PortalContainer();
        slots = alienSlots;
    }

    //Gets the active slot from PortalPanel
    public void SetSlot(ItemSlot slot)
    {
        activeSlot = slot as AlienSlot;
    }

    public override bool AddItem(Item item)
    {
        if (container.AddItem(item, GetIndexOfActiveSlot()))
        {
            AddItemToUI(item);
            return true;
        }
        return false;
    }

    private void AddItemToUI(Item item)
    {
        slots[GetIndexOfActiveSlot()].AddItemToSlot(item);
    }

    public override void RemoveItem(Item item)
    {
        container.RemoveItem(item);
        RemoveItemFromUI();
    }

    public override void RemoveItem(ItemAsset item)
    {
        container.RemoveItem(item);
        RemoveItemFromUI();
    }

    private void RemoveItemFromUI()
    {
        slots[GetIndexOfActiveSlot()].ClearSlot();
    }

    protected override void UpdateUI(Item item)
    {
        //split up in AddItem and RemoveItem
    }

    //Get the index of where the active Slot is in the slots array
    private int GetIndexOfActiveSlot()
    {
        Debug.Log("Active Slot: " + activeSlot.name);
        //Debug.Log("Slots: " + slots.Length);
        for (int i = 0; i < slots.Length; i++)
        {
            //Debug.Log("Comparing to " + slots[i]);
            if(slots[i] == activeSlot)
            {
                return i;
            }
        }
        return -1;
    }
}
