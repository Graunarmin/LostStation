using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public delegate void FilterEquipped();
    public static event FilterEquipped OnFilterEquipped;
    [SerializeField] FlashlightFilter flashlightFilter;

    private void Update()
    {
        if (GameManager.gameManager.InventoryOpen() && Input.GetKeyDown(KeyCode.E))
        {
            if (InventoryManager.invManager.ContainerContainsItem(flashlightFilter))
            {
                flashlightFilter.Equip();
                InventoryManager.invManager.HideDescription();
                if (OnFilterEquipped != null)
                {
                    OnFilterEquipped();
                }
            }
        }
    }

    [SerializeField] List<Button> craftingButtons = new List<Button>();

    protected override void Start()
    {
        base.Start();

        foreach(Button button in craftingButtons)
        {
            button.gameObject.SetActive(false);
        }
    }

    public void ResetButtons()
    {
        foreach (Button button in craftingButtons)
        {
            button.gameObject.SetActive(false);
        }
    }


}
