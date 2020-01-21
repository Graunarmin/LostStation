using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashDoorCanvas : MonoBehaviour
{
    public void Activate()
    {
        //GameManager.gameManager.SwitchCameras("2D");
        gameObject.SetActive(true);
        //Time.timeScale = 0f;
    }

    public void Close()
    {
        //GameManager.gameManager.SwitchCameras("3D");
        gameObject.SetActive(false);
        //Time.timeScale = 1f;
    }

    public void TryAgain()
    {
        Close();
        //and return to keypad (should work?)
    }

    public void Smash()
    {
        if(Reference.instance.collectHeld != null)
        {
            if (Reference.instance.collectHeld.name == "Crowbar")
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
