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
        //get the right Pillar

        shade.gameObject.SetActive(true);
        //Cameras are already managed in InventoryManager
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
        air.gameObject.SetActive(false);
        water.gameObject.SetActive(false);
        fire.gameObject.SetActive(false);
        earth.gameObject.SetActive(false);
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

    
}
