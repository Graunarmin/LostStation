﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    #region Singleton
    public static InventoryManager invManager;
    private void Awake()
    {

        if (invManager == null)
        {
            invManager = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of Inventory!");
        }

    }
    #endregion

    public Transform itemsParent;
    public int space = 6;
    public List<Collectable> items = new List<Collectable>();

    public Image newItemInfo;

    InventorySlot[] slots;

    void Start()
    {
        slots = itemsParent.GetComponentsInChildren<InventorySlot>();
    }

    public bool AddItem(Collectable item)
    {
        //Check if this was the first collectable and if so
        //show info on how to access inventory
        TutorialManager.tutorialManager.FirstCollectable();

        if(items.Count >= space)
        {
            Debug.Log("Not enough room");
            return false;
        }

        items.Add(item);
        UpdateUI();
        return true;
    }

    public void RemoveItem(Collectable item)
    {
        items.Remove(item);
        UpdateUI();
    }

    public void OpenInventory()
    {
        if (Reference.instance.inventory.gameObject.activeInHierarchy)
        {
            CloseInventory();
        }
        else
        {
            GameManager.gameManager.SwitchCameras("2D");
            Reference.instance.inventoryCanvas.gameObject.SetActive(true);
            Reference.instance.inventory.gameObject.SetActive(true);
        }
    }

    public void CloseInventory()
    {
        if (!GameManager.gameManager.InspectorOpen())
        {
            GameManager.gameManager.SwitchCameras("3D");
        }
        Reference.instance.inventoryCanvas.gameObject.SetActive(false);
        Reference.instance.inventory.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < items.Count)
            {
                slots[i].AddItemToSlot(items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        Debug.Log("Starting Coroutine");
        StartCoroutine(ShowUpdateIcon());
    }

    private IEnumerator ShowUpdateIcon()
    {
        //wait until the player is back in the game
        yield return new WaitUntil(()
            => !GameManager.gameManager.CurrentlyInteracting());

        Debug.Log("Show Update Icon");
        //show pop-up that journal was updated
        Reference.instance.inventoryCanvas.gameObject.SetActive(true);
        newItemInfo.gameObject.SetActive(true);
        //PlaySound
        //...

        yield return new WaitForSeconds(2);

        //hide pop-up
        Reference.instance.inventoryCanvas.gameObject.SetActive(false);
        newItemInfo.gameObject.SetActive(false);
    }

}