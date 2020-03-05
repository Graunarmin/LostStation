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
    [SerializeField] Image shade;

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

    public void Close()
    {
        gameObject.SetActive(false);
        shade.gameObject.SetActive(false);
        DeactivatePillars();
        //Cameras are already managed in InventoryManager
        InventoryManager.invManager.CloseInventory();
        CraftingManager.craftManager.ResetButtons();
        //so the inventory can again be opened by pressing I
        GameManager.gameManager.SetInventoryKey(KeyCode.I);
        Time.timeScale = 1f;

    }

    public void ActivatePillar(Item item)
    {
        if(item.itemInfo.itemName == "AirPillar")
        {
            Debug.Log("Activate Air Pillar");
            air.gameObject.SetActive(true);
            PortalManager.portal.SetSlot(air);
        }
        else if (item.itemInfo.itemName == "WaterPillar")
        {
            Debug.Log("Activate Water Pillar");
            water.gameObject.SetActive(true);
            PortalManager.portal.SetSlot(water);
        }
        else if (item.itemInfo.itemName == "FirePillar")
        {
            Debug.Log("Activate Fire Pillar");
            fire.gameObject.SetActive(true);
            PortalManager.portal.SetSlot(fire);
        }
        else if (item.itemInfo.itemName == "EarthPillar")
        {
            Debug.Log("Activate Earth Pillar");
            earth.gameObject.SetActive(true);
            PortalManager.portal.SetSlot(earth);
        }
    }

    private void DeactivatePillars()
    {
        air.gameObject.SetActive(false);
        water.gameObject.SetActive(false);
        fire.gameObject.SetActive(false);
        earth.gameObject.SetActive(false);
        PortalManager.portal.SetSlot(null);
    }

}
