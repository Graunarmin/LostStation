using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingManager : ItemContainerManager
{
    #region Singleton
    public static CraftingManager craftManager;
    private void Awake()
    {

        if (craftManager == null)
        {
            craftManager = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of CraftinManager!");
        }

    }
    #endregion

    [SerializeField] FlashlightFilter flashlightFilter;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (InventoryManager.invManager.ContainerContainsItem(flashlightFilter))
            {
                flashlightFilter.Equip();
            }
        }
    }


}
