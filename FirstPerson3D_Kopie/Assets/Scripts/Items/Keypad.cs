using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : Item
{
    public Door door;
    public string password;
    private bool passwordCorrect;

    public override void ManageInteractables()
    {
        if (!door.DoorIsUnlocked())
        {
            //make item interactable, if prerequisite is met
            if (interactable != null)
            {
                ManageJournalInfo();
                Debug.Log("Current Keypad: " + name);
                Reference.instance.currentKeypad = this;
                if (AllPrerequsComplete())
                {
                    Reference.instance.keyPad.gameObject.SetActive(true);
                    interactable.enabled = true;
                    interactable.Interact();
                }           
            }
        }
        else
        {
            Debug.Log("Door was already unlocked!");
        }
        
    }

    public void SetPasswordCorrect()
    {
        passwordCorrect = true;
        location.RemoveItem(this);
    }

    public bool PasswordCorrect()
    {
        return passwordCorrect;
    }

    public string GetPassword()
    {
        return password;
    }

}
