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

    public bool AllAliensInside()
    {
        return (container.Size() == 3);
    }

    private bool CorrectOrder()
    {
        List<Item> testList = container.GetContainer();
        if((testList[0].itemInfo.itemName == "FireAlien" || testList[0].itemInfo.itemName == "WaterAlien")
            && (testList[1].itemInfo.itemName == "FireAlien" || testList[1].itemInfo.itemName == "WaterAlien")
            && testList[2].itemInfo.itemName == "AirAlien")
        {
            return true;
        }
        return false;
    }

    private bool AliensPlacedCorrectly()
    {
        return (portalPanel.AliensInRightSlot() &&
               activeSlot.slotName == "Earth");
    }

    public void CheckSolution()
    {
        if (AliensPlacedCorrectly() && CorrectOrder())
        {
            Debug.Log("Loading ...");
        }
        else
        {
            Debug.Log("Error!");
        }
    }

    public void OpenControls()
    {
        //open up canvas with controls
        //React to Input
        //Check Solution
        CheckSolution();
    }
}
