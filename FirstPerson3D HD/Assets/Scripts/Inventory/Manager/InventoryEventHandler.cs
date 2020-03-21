using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryEventHandler : MonoBehaviour
{
    #region Singleton
    public static InventoryEventHandler inventoryEvents;
    private void Awake()
    {

        if (inventoryEvents == null)
        {
            inventoryEvents = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of Inventory EventHandler!");
        }

    }
    #endregion

    [SerializeField] Image draggableItem;
    [SerializeField] ResultSlot resultSlot;
    private ItemSlot dragItemSlot;

    public void BeginDrag(ItemSlot itemSlot, BaseEventData data)
    {
        //only drag if this item is a crafting Material, a result item or an alien
        if ((GameManager.gameManager.Crafting() || GameManager.gameManager.PortalPuzzle())
            && itemSlot.GetItem() != null)
        {
            if (itemSlot.GetItem().itemInfo.craftingMaterial || itemSlot.GetItem().itemInfo.isResultItem || itemSlot.GetItem() is Alien)
            {
                //cast data as PointerEventData to get Cursor Position
                PointerEventData pointerData = data as PointerEventData;

                //dragged Slot = der, der grade gezogen wird
                dragItemSlot = itemSlot;
                //icon des itemSlots verbergen
                itemSlot.icon.enabled = false;

                //das draggable item bekommt das icon, das gezogen wird
                draggableItem.sprite = itemSlot.icon.sprite;
                //draggableItem wird mitgezogen und enabled
                draggableItem.transform.position = Reference.instance.camera2D.ScreenToWorldPoint(pointerData.position);
                draggableItem.transform.position = new Vector3(draggableItem.transform.position.x, draggableItem.transform.position.y, 100f);
                draggableItem.gameObject.SetActive(true);
                draggableItem.gameObject.GetComponent<Image>().enabled = true;
            }
        }
    }

    public void Drag(ItemSlot itemSlot, BaseEventData data)
    {
        //Drag Item along
        //draggableItem is only enabled, if it is a crafing material
        if (draggableItem.gameObject.activeInHierarchy)
        {
            //cast as PointerEventData to get Cursor Position
            PointerEventData pointerData = data as PointerEventData;
            draggableItem.transform.position = Reference.instance.camera2D.ScreenToWorldPoint(pointerData.position);
            draggableItem.transform.position = new Vector3(draggableItem.transform.position.x, draggableItem.transform.position.y, 100f);
        }
    }

    public void EndDrag(ItemSlot itemSlot)
    {
        //Called from the the starting slot
        if((GameManager.gameManager.Crafting() || GameManager.gameManager.PortalPuzzle())
            && itemSlot.GetItem() != null)
        {
            itemSlot.icon.enabled = true;
        }
        //hier wird nur der dragged Slot zurück gesetzt
        dragItemSlot = null;
        //und das draggableItem wird disabled
        draggableItem.sprite = null;
        draggableItem.gameObject.GetComponent<Image>().enabled = false;
        draggableItem.gameObject.SetActive(false);
    }

    public void Drop(ItemSlot dropItemSlot)
    {
        if((GameManager.gameManager.Crafting() || GameManager.gameManager.PortalPuzzle())
            && dragItemSlot != null)
        {
            if (dropItemSlot.CanReceiveItem(dragItemSlot.GetItem()) )
            {
                //drag from Inventory to crafting area
                if (dropItemSlot is CraftingSlot && dragItemSlot is InventorySlot && resultSlot.IsEmpty())
                {
                    AudioManager.audioManager.PlaySound(AudioManager.audioManager.craftingSlot);
                    CraftingManager.craftManager.AddItem(dragItemSlot.GetItem());
                    InventoryManager.invManager.RemoveItem(dragItemSlot.GetItem());
                }
                //drag from crafting area to inventory
                else if (dropItemSlot is InventorySlot && dragItemSlot is CraftingSlot && resultSlot.IsEmpty())
                {
                    AudioManager.audioManager.PlaySound(AudioManager.audioManager.craftingSlot);
                    InventoryManager.invManager.AddItem(dragItemSlot.GetItem());
                    CraftingManager.craftManager.RemoveItem(dragItemSlot.GetItem());
                }
                //drag from result to inventory
                else if (dropItemSlot is InventorySlot && dragItemSlot is ResultSlot)
                {
                    AudioManager.audioManager.PlaySound(AudioManager.audioManager.getResult);
                    InventoryManager.invManager.AddItem(dragItemSlot.GetItem());
                    ResultManager.resManager.RemoveItem(dragItemSlot.GetItem());
                }
                //drag from inventory to pillar
                else if (dropItemSlot is AlienSlot && dragItemSlot is InventorySlot)
                {
                    AudioManager.audioManager.PlaySound(AudioManager.audioManager.craftingSlot);
                    PortalManager.portal.AddItem(dragItemSlot.GetItem());
                    InventoryManager.invManager.RemoveItem(dragItemSlot.GetItem());
                }
                //drag from pillar back to inventory
                else if (dropItemSlot is InventorySlot && dragItemSlot is AlienSlot)
                {
                    AudioManager.audioManager.PlaySound(AudioManager.audioManager.craftingSlot);
                    InventoryManager.invManager.AddItem(dragItemSlot.GetItem());
                    PortalManager.portal.RemoveItem(dragItemSlot.GetItem());
                }
            }
            
        }
  
    }
}
