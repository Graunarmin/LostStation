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

    [SerializeField] PortalPanel portalPanel;

    private AlienSlot activeSlot;

    protected override void Start()
    {
        container = new PortalContainer();
        slots = portalPanel.GetPillars();
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
            UpdateUI(item, true);
            return true;
        }
        return false;
    }

    public override void RemoveItem(Item item)
    {
        container.RemoveItem(item);
        UpdateUI(null, false);
    }

    public override void RemoveItem(ItemAsset item)
    {
        container.RemoveItem(item);
        UpdateUI(null, false);
    }

    protected override void UpdateUI(Item item, bool add)
    {
        if (add)
        {
            slots[GetIndexOfActiveSlot()].AddItemToSlot(item);
        }
        else
        {
            slots[GetIndexOfActiveSlot()].ClearSlot();
        }
    }

    //Get the index of where the active Slot is in the slots array
    private int GetIndexOfActiveSlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i] == activeSlot)
            {
                return i;
            }
        }
        return -1;
    }
}
