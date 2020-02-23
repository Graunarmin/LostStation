using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingInspector : Interactable
{
    public CraftingPanel craftingPanel;

    public override void Interact()
    {
        craftingPanel.Activate();
    }

    
}
