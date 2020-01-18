using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Item
{
    public Door door;
    public string password;
    public bool passwordCorrect;

    public override void ManageInteractables()
    {
        if (!door.doorUnlocked)
        {
            //make item interactable, if prerequisite is met
            if (interactable != null)
            {
                Debug.Log("Current Keypad: " + name);
                Reference.instance.currentKeypad = this;
                if (AllPrerequsComplete())
                {
                    Reference.instance.keyPad.gameObject.SetActive(true);
                    interactable.enabled = true;
                    CheckForCollectable();
                    interactable.Interact();
                }
                    
           
            }

        }
        else
        {
            Debug.Log("Door was already unlocked!");
        }
        
    }

}
