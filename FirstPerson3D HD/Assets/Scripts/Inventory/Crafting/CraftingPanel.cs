using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingPanel : MonoBehaviour, IPuzzleCanvas
{
    [SerializeField] Image shade;

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

    public void Close()
    {
        gameObject.SetActive(false);
        shade.gameObject.SetActive(false);
        //Cameras are already managed in InventoryManager
        InventoryManager.invManager.CloseInventory();
        CraftingManager.craftManager.ResetButtons();
        //so the inventory can again be opened by pressing I
        GameManager.gameManager.SetInventoryKey(KeyCode.I);
        Time.timeScale = 1f;
    }

    public void SetButton(Button button)
    {
        button.gameObject.SetActive(true);
    }
}
