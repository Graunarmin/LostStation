using System;

public class InventorySlot : ItemSlot
{

    public override void Hover()
    {
        
        if (!GameManager.gameManager.Crafting() && !GameManager.gameManager.PortalPuzzle())
        {
            if (item != null)
            {
                //Show Info
                InventoryManager.invManager.ShowDescription(item);
            }
        }
    }

    public override void HoverExit()
    {
        if (!GameManager.gameManager.Crafting() && !GameManager.gameManager.PortalPuzzle())
        {
            //Hide Info
            InventoryManager.invManager.HideDescription();
        }
    }

}
