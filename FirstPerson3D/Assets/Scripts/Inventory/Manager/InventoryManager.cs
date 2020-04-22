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

    [SerializeField] Canvas newItemInfo;
    [SerializeField] GameObject descriptionPanel;
    [SerializeField] TextMeshProUGUI itemDescription;
    [SerializeField] Canvas warning;
    [SerializeField] GameObject inventoryFull;
    [SerializeField] GameObject cantCraft;
    [SerializeField] GameObject alreadyCollected;
    [SerializeField] int inventorySize;
    [SerializeField] List<ItemSlot> page2Slots;
    [SerializeField] Button next;
    [SerializeField] Button prev;
    private List<ItemSlot> page1Slots;

    //notify everyone who needs the keycard
    public delegate void KeyCardCollected();
    public static event KeyCardCollected OnKeyCardCollected;

    protected override void Start()
    {
        container = new Inventory();
        //activate all slots so they can be added to slots array
        TogglePage2(true);
        //get all the available slots
        slots = itemsParent.GetComponentsInChildren<ItemSlot>();
        //and deactivte page 2 again so we only see the first page
        TogglePage2(false);
        container.SetSpace(inventorySize);
        page1Slots = new List<ItemSlot>(itemsParent.GetComponentsInChildren<ItemSlot>());

        //deactivate everything not visible in the beginning
        descriptionPanel.SetActive(false);
        warning.gameObject.SetActive(false);
        inventoryFull.gameObject.SetActive(false);
        cantCraft.gameObject.SetActive(false);
        alreadyCollected.gameObject.SetActive(false);
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
        return false;
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
        if (!GameManager.gameManager.PortalPuzzle() && !GameManager.gameManager.Crafting())
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

    public void TogglePage1(bool open)
    {
        foreach (ItemSlot slot in page1Slots)
        {
            slot.gameObject.SetActive(open);
        }
        //if page1 slots are being activated, we need the Button that leads to page2
        if (open)
        {
            next.gameObject.SetActive(true);
        }
        else
        {
            next.gameObject.SetActive(false);
        }
    }

    public void TogglePage2(bool open)
    {
        foreach (ItemSlot slot in page2Slots)
        {
            slot.gameObject.SetActive(open);
        }
        //if page2 slots are being activated, we need the Button that leads back to page 1
        if (open)
        {
            prev.gameObject.SetActive(true);
        }
        else
        {
            prev.gameObject.SetActive(false);
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

    #region Warnings
    public void BackpackFull()
    {
        StartCoroutine(ShowWarning(inventoryFull));
    }

    public void CantCraft()
    {
        StartCoroutine(ShowWarning(cantCraft));
    }
    public void AlreadyCollected()
    {
        StartCoroutine(ShowWarning(alreadyCollected));
    }

    private IEnumerator ShowWarning(GameObject text)
    {
        //Show Info that Backpack is fulll
        warning.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        text.gameObject.SetActive(false);
        warning.gameObject.SetActive(false);
    }
    #endregion

}