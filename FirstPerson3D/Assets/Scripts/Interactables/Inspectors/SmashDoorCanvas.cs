using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashDoorCanvas : MonoBehaviour
{
    public Collectable crowbar;
    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    public void TryAgain()
    {
        Close();
    }

    public void Smash()
    {
        if(InventoryManager.invManager.items.Count > 0)
        {
            if (InventoryManager.invManager.items.Contains(crowbar))
            {
                Reference.instance.keyPad.SmashedKeyPad();
                //Add journalPag "yay"
            }
            else
            {
                Reference.instance.keyPad.Close();
                //Add Journalpage "ouw"
            }
        }
        else
        {
            Reference.instance.keyPad.Close();
            //Add Journalpage "ouw"
        }
        Close();
        
    }

    public void Leave()
    {
        Reference.instance.keyPad.Close();
        Close();
    }
}
