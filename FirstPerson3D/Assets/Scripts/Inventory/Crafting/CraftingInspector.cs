using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingInspector : Interactable
{
    public CraftingPanel craftingPanel;
    public Button craftingButton;

    public override void Interact()
    {
        craftingPanel.Activate();
        craftingPanel.SetButton(craftingButton);
    }

    
}
