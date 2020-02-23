using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingPanel : MonoBehaviour, IPuzzleCanvas
{ 
    public void Activate()
    {
        gameObject.SetActive(true);
        //Cameras are already managed in InventoryManager
        InventoryManager.invManager.OpenInventory();
        //so the inventory can no longer be opened by pressing I
        GameManager.gameManager.SetInventoryKey(KeyCode.None);
        Time.timeScale = 0f;
    }

    public void Close()
    {
        gameObject.SetActive(false);
        //Cameras are already managed in InventoryManager
        InventoryManager.invManager.CloseInventory();
        //so the inventory can again be opened by pressing I
        GameManager.gameManager.SetInventoryKey(KeyCode.I);
        Time.timeScale = 1f;
    }
}
