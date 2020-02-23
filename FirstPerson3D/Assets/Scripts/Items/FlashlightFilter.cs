using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightFilter : Item
{
    [SerializeField] Item frame;

    public void Equip()
    {
        //Remove Filter from inventory
        InventoryManager.invManager.RemoveItem(this);

        //change color of flashlight

        //make the aliens visible

        //start timer until Unequip
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(120);
        ShowCracks();

        yield return new WaitForSeconds(30);

        Unequip();
        
    }

    private void ShowCracks()
    {
        //show first cracks in Lense
    }

    public void Unequip()
    {
        //stop timer
        StopCoroutine(Timer());

        //make aliens invisible

        //change color of flashlight back to normal

        //put frame into inventory
        InventoryManager.invManager.AddItem(frame);
    }
}
