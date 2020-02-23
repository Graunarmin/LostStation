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
    private ItemSlot dragItemSlot;

    public void BeginDrag(ItemSlot itemSlot, BaseEventData data)
    {
        //only drag if this item is a crafting Material!
        if (itemSlot.GetItem() != null)
        {
            if (itemSlot.GetItem().itemInfo.craftingMaterial || itemSlot.GetItem().itemInfo.isResultItem)
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
        if (draggableItem.enabled)
        {
            //cast as PointerEventData to get Cursor Position
            PointerEventData pointerData = data as PointerEventData;
            draggableItem.transform.position = Reference.instance.camera2D.ScreenToWorldPoint(pointerData.position);
            draggableItem.transform.position = new Vector3(draggableItem.transform.position.x, draggableItem.transform.position.y, 100f);
        }
    }

    public void EndDrag(ItemSlot itemSlot)
    {
        //Counts the starting slot
        if(itemSlot.GetItem() != null)
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
        if(dragItemSlot != null)
        {
            if (dropItemSlot.CanReceiveItem(dragItemSlot.GetItem()))
            {
                //drag from Inventory to crafting area
                if(dropItemSlot is CraftingSlot && dragItemSlot is InventorySlot)
                {
                    CraftingManager.craftManager.AddItem(dragItemSlot.GetItem());
                    InventoryManager.invManager.RemoveItem(dragItemSlot.GetItem());
                }
                //drag from crafting area to inventory
                else if(dropItemSlot is InventorySlot && dragItemSlot is CraftingSlot)
                {
                    InventoryManager.invManager.AddItem(dragItemSlot.GetItem());
                    CraftingManager.craftManager.RemoveItem(dragItemSlot.GetItem());
                }
                else if(dropItemSlot is InventorySlot && dragItemSlot is ResultSlot)
                {
                    InventoryManager.invManager.AddItem(dragItemSlot.GetItem());
                    ResultManager.resManager.RemoveItem(dragItemSlot.GetItem());
                }
                else
                {
                    //let icon snap back to it's original position
                }
                
            }
        }
    }

}
