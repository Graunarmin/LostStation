using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemContainerManager : MonoBehaviour
{
    protected IItemContainer container;
    protected ItemSlot[] slots;

    public Transform itemsParent;

    protected virtual void Start()
    {
        container = new Inventory();
        slots = itemsParent.GetComponentsInChildren<ItemSlot>();
        container.SetSpace(2);
    }

    public virtual bool AddItem(Item item)
    {
        if (container.AddItem(item))
        {
            UpdateUI();
            return true;
        }
        return false;
    }

    public virtual void RemoveItem(Item item)
    {
        container.RemoveItem(item);
        UpdateUI();
    }

    public virtual void RemoveItem(ItemAsset item)
    {
        container.RemoveItem(item);
        UpdateUI();
    }

    protected virtual void UpdateUI(Item item = null, bool add = false)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(i < container.Size())
            {
                slots[i].AddItemToSlot(container.GetItemAtIndex(i));
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }

    public int GetContainerSize()
    {
        return container.Size();
    }

    public bool ContainerContainsItem(Item item)
    {
        if (container.ContainsItem(item))
        {
            return true;
        }
        return false;
    }

    public bool ContainerContainsItem(ItemAsset item)
    {
        if (container.ContainsItem(item))
        {
            return true;
        }
        return false;
    }

    public IItemContainer GetContainer()
    {
        return container;
    }
}
