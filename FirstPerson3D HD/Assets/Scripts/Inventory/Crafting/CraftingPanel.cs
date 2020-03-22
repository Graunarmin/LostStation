using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour, IPuzzleCanvas
{
    [SerializeField] Image shade;
    [SerializeField] ResultSlot resultSlot;

    public void Activate()
    {
        //CraftingInspector inspector = interactable as CraftingInspector;

        gameObject.SetActive(true);
        shade.gameObject.SetActive(true);
        //Cameras are already managed in InventoryManager
        InventoryManager.invManager.OpenInventory();
        //so the inventory can no longer be opened by pressing I
        GameManager.gameManager.SetInventoryKey(KeyCode.None);
        Time.timeScale = 0f;
    }

    public bool Close()
    {
        //Don't close if the result was not yet added to backpack
        if(resultSlot.GetItem() != null)
        {
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.inventoryFull);
            Debug.Log("Please clear result slot before closing");
            return false;
        }
        //else if the slot is empty, close everything;
        gameObject.SetActive(false);
        shade.gameObject.SetActive(false);
        //Cameras are already managed in InventoryManager
        InventoryManager.invManager.CloseInventory();
        CraftingManager.craftManager.ResetButtons();
        //so the inventory can again be opened by pressing I
        GameManager.gameManager.SetInventoryKey(KeyCode.I);
        Time.timeScale = 1f;
        return true;
    }

    public void SetButton(Button button)
    {
        button.gameObject.SetActive(true);
    }
}
