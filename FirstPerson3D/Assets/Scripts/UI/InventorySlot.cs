using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Collectable item;
    public Image icon;
    //public Button removeButton;

    public void AddItemToSlot(Collectable newItem)
    {
        item = newItem;

        icon.sprite = item.itemInfo.icon;
        icon.enabled = true;
        //removeButton.interactable = true;
    }

    public void ClearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        //removeButton.interactable = true;
    }

    //public void OnRemoveButton()
    //{
    //    InventoryManager.invManager.RemoveItem(item);
    //}
}
