using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingButton : MonoBehaviour
{
    //Holds a List of Recipes and on click checks if one of it can be crafted

    [SerializeField] List<CraftingRecipe> recipes = new List<CraftingRecipe>();
    private IItemContainer materials;

    public void OnClick()
    {
        materials = CraftingManager.craftManager.GetContainer();

        foreach(CraftingRecipe recipe in recipes)
        {
            recipe.Craft(materials);
            
        }
    }
}
