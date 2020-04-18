using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortalPanel : MonoBehaviour, IPuzzleCanvas
{
    [SerializeField] AlienSlot air;
    [SerializeField] AlienSlot water;
    [SerializeField] AlienSlot fire;
    [SerializeField] AlienSlot earth;

    [SerializeField] CorrectSolutionReaction airReaction;
    [SerializeField] CorrectSolutionReaction waterReaction;
    [SerializeField] CorrectSolutionReaction fireReaction;
    [SerializeField] CorrectSolutionReaction earthReaction;


    [SerializeField] Sprite airPillarIcon;
    [SerializeField] Sprite waterPillarIcon;
    [SerializeField] Sprite firePillarIcon;
    [SerializeField] Sprite earthPillarIcon;

    [SerializeField] AlienSlot submitButton;

    [SerializeField] Image shade;
    [SerializeField] JournalPage journalPage;

    private bool firstPillarClicked;


    public void Activate()
    {
        gameObject.SetActive(true);
        shade.gameObject.SetActive(true);

        //ActivatePillar is called from PillarInspector as only that class knows which one was clicked
        //Cameras are already managed in InventoryManager

        //Everythin takes place "inside" the inventory
        InventoryManager.invManager.OpenInventory();
        //so the inventory can no longer be opened by pressing I
        GameManager.gameManager.SetInventoryKey(KeyCode.None);
        Time.timeScale = 0f;
    }

    public bool Close()
    {
        gameObject.SetActive(false);
        shade.gameObject.SetActive(false);
        DeactivatePillars();
        //Cameras are already managed in InventoryManager
        //This Canvas is always closed by InventoryManager!
        //InventoryManager.invManager.CloseInventory();
        //so the inventory can again be opened by pressing I
        GameManager.gameManager.SetInventoryKey(KeyCode.I);
        Time.timeScale = 1f;
        return true;
    }

    public ItemSlot[] GetPillars()
    {
        return new ItemSlot[] { air, fire, water, earth };
    }

    //show (activate) the respective ItemSlot of the selected pillar
    public void ActivatePillar(Item item)
    {
        if (!firstPillarClicked)
        {
            GameManager.gameManager.FireNewJournalEntry(journalPage);
            firstPillarClicked = true;
        }

        if(item.itemInfo.itemName == "AirPillar")
        {
            gameObject.GetComponent<Image>().sprite = airPillarIcon;
            PortalManager.portal.SetSlot(air);
            if (ActivateSubmitButton(air)) { }
            else
            {
                air.gameObject.SetActive(true);
            }
        }
        else if (item.itemInfo.itemName == "WaterPillar")
        {
            gameObject.GetComponent<Image>().sprite = waterPillarIcon;
            PortalManager.portal.SetSlot(water);
            if (ActivateSubmitButton(water)) { }
            else
            {
                water.gameObject.SetActive(true);
            }
        }
        else if (item.itemInfo.itemName == "FirePillar")
        {
            gameObject.GetComponent<Image>().sprite = firePillarIcon;
            PortalManager.portal.SetSlot(fire);
            if (ActivateSubmitButton(fire)) { }
            else
            {
                fire.gameObject.SetActive(true);
            }
        }
        else if (item.itemInfo.itemName == "EarthPillar")
        {
            gameObject.GetComponent<Image>().sprite = earthPillarIcon;
            PortalManager.portal.SetSlot(earth);
            if (ActivateSubmitButton(earth)) { }
            else
            {
                earth.gameObject.SetActive(true);
            }
        }
    }

    private bool ActivateSubmitButton(AlienSlot slot)
    {
        if (PortalManager.portal.AllAliensInside() && slot.GetItem() == null)
        {
            submitButton.gameObject.SetActive(true);
            return true;
        }
        return false;
    }

    private void DeactivatePillars()
    {
        if(PortalManager.portal.ActiveSlotName() == "Air")
        {
            DeactivateAir();
        }else if(PortalManager.portal.ActiveSlotName() == "Water")
        {
            DeactivateWater();
        }else if(PortalManager.portal.ActiveSlotName() == "Fire")
        {
            DeactivateFire();
        }else if(PortalManager.portal.ActiveSlotName() == "Earth")
        {
            DeactivateEarth();
        }

        submitButton.gameObject.SetActive(false);
        PortalManager.portal.SetSlot(null);
        gameObject.GetComponent<Image>().sprite = null;
    }

    public bool AliensInRightSlot()
    {
        if(air.GetItem() != null && water.GetItem() != null &&
           fire.GetItem() != null && earth.GetItem() == null)
        {
            if (air.GetItem().itemInfo.itemName == "AirAlien" &&
                water.GetItem().itemInfo.itemName == "WaterAlien" &&
                fire.GetItem().itemInfo.itemName == "FireAlien")
            {
                return true;
            }
        }
        return false;
    }

    private void DeactivateAir()
    {
        
        if(air.GetItem() != null)
        {
            if(air.GetItem().itemInfo.itemName == "AirAlien")
            {
                airReaction.React();
            }
            else
            {
                airReaction.UndoReaction();
            }
        }
        else
        {
            airReaction.UndoReaction();
        }
        air.gameObject.SetActive(false);
    }

    private void DeactivateWater()
    {

        if (water.GetItem() != null)
        {
            if (water.GetItem().itemInfo.itemName == "WaterAlien")
            {
                waterReaction.React();
            }
            else
            {
                waterReaction.UndoReaction();
            }
        }
        else
        {
            waterReaction.UndoReaction();
        }
        water.gameObject.SetActive(false);
    }

    private void DeactivateFire()
    {

        if (fire.GetItem() != null)
        {
            if (fire.GetItem().itemInfo.itemName == "FireAlien")
            {
                fireReaction.React();
            }
            else
            {
                fireReaction.UndoReaction();
            }
        }
        else
        {
            fireReaction.UndoReaction();
        }
        fire.gameObject.SetActive(false);
    }

    private void DeactivateEarth()
    {

        if (earth.GetItem() != null)
        {
            if (earth.GetItem().itemInfo.itemName == "EarthAlien")
            {
                earthReaction.React();
            }
            else
            {
                earthReaction.UndoReaction();
            }
        }
        else
        {
            earthReaction.UndoReaction();
        }
        earth.gameObject.SetActive(false);
    }


}
