using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : ItemContainerManager
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

    public Canvas newItemInfo;
    public GameObject descriptionPanel;
    public TextMeshProUGUI itemDescription;
    public Canvas warning;
    public GameObject inventoryFull;
    public GameObject cantCraft;

    //notify everyone who needs the keycard
    public delegate void KeyCardCollected();
    public static event KeyCardCollected OnKeyCardCollected;

    protected override void Start()
    {
        base.Start();
        container.SetSpace(4);
        descriptionPanel.SetActive(false);
    }

    //is called by Collector
    public override bool AddItem(Item item)
    {
        if (container.AddItem(item))
        {
            if (item.itemInfo.name == "Keycard")
            {
                if (OnKeyCardCollected != null)
                {
                    OnKeyCardCollected();
                }
            }
            UpdateUI();
            return true;
        }
        //StartCoroutine(ShowWarning());
        return false;
    }

    private IEnumerator ShowWarning()
    {
        warning.gameObject.SetActive(true);
        inventoryFull.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        warning.gameObject.SetActive(false);
        inventoryFull.gameObject.SetActive(false);
    }

    //not called yet
    public override void RemoveItem(Item item)
    {
        container.RemoveItem(item);
        UpdateUI();
    }

    protected override void UpdateUI(Item item = null, bool add = false)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i < container.Size())
            {
                slots[i].AddItemToSlot(container.GetItemAtIndex(i));
                if (!GameManager.gameManager.Crafting() && !GameManager.gameManager.PortalPuzzle())
                {
                    StartCoroutine(ShowUpdateIcon());
                }
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
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
        if(!GameManager.gameManager.PortalPuzzle() && !GameManager.gameManager.Crafting())
        {
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.openInventory);
        }
    }

    public void CloseInventory()
    {
        bool craftingSlotEmpty = true;
        if (Reference.instance.craftingArea.gameObject.activeInHierarchy)
        {
            craftingSlotEmpty = Reference.instance.craftingArea.Close();
        }
        if (Reference.instance.portalPanel.gameObject.activeInHierarchy)
        {
            Reference.instance.portalPanel.Close();
        }
        if (craftingSlotEmpty)
        {
            HideDescription();
            Reference.instance.inventoryCanvas.gameObject.SetActive(false);
            Reference.instance.inventory.gameObject.SetActive(false);
            //Debug.Log("Closing Inventory");
            GameManager.gameManager.SwitchCameras("3D");
        }
    }

    public void ShowDescription(Item item)
    {
        descriptionPanel.SetActive(true);
        itemDescription.text = item.itemInfo.descriptionText;
    }

    public void HideDescription()
    {
        descriptionPanel.SetActive(false);
        itemDescription.text = "";
    }

    private IEnumerator ShowUpdateIcon()
    {
        //wait until the player is back in the game
        yield return new WaitUntil(()
            => !GameManager.gameManager.CurrentlyInteracting());

        //Debug.Log("Show Update Icon");
        //show pop-up that journal was updated
        AudioManager.audioManager.PlaySound(AudioManager.audioManager.newItem);
        newItemInfo.gameObject.SetActive(true);

        yield return new WaitForSeconds(3);

        //hide pop-up
        newItemInfo.gameObject.SetActive(false);
    }

}