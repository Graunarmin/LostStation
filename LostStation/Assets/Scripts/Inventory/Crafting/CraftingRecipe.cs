using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public struct CraftingMaterial
{
    public ItemAsset item;
}

[CreateAssetMenu(fileName = "New Crafting Recipe", menuName = "Crafting Recipe")]
public class CraftingRecipe : ScriptableObject
{
    [SerializeField]
    private List<CraftingMaterial> neededMaterials;
    [SerializeField]
    private Item result;

    public bool CanCraft(IItemContainer itemContainer)
    {
        foreach(CraftingMaterial material in neededMaterials)
        {
            if (!itemContainer.ContainsItem(material.item))
            {
                Debug.Log("Can't Craft");
                return false;
            }
        }
        return true;
    }

    public bool Craft(IItemContainer itemContainer)
    {
        if (CanCraft(itemContainer))
        {
            Debug.Log("Crafting!");
            foreach (CraftingMaterial material in neededMaterials)
            {
                if (material.item.isConsumed)
                {
                    CraftingManager.craftManager.RemoveItem(material.item);
                }
            }

            //Add result to Result-Slot
            ResultManager.resManager.AddItem(result);
            return true;
        }
        return false;
    }

  
}
