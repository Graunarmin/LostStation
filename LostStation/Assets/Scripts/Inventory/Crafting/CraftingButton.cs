using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingButton : MonoBehaviour
{
    //Holds a List of Recipes and on click checks if one of it can be crafted

    [SerializeField] List<CraftingRecipe> recipes = new List<CraftingRecipe>();
    private IItemContainer materials;

    private List<bool> canCraft = new List<bool>();

    public void OnClick()
    {
        materials = CraftingManager.craftManager.GetContainer();

        foreach (CraftingRecipe recipe in recipes)
        {
            canCraft.Add(recipe.Craft(materials));

        }
        if (canCraft.Contains(true))
        {
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.crafting);
        }
        else
        {
            InventoryManager.invManager.CantCraft();
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.craftingImpossible);
        }
        canCraft.Clear();
    }
}