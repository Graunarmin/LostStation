using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
    //the keypad that unlocks the door (if any)
    public Keypad connectedKeypad;

    //indicates if the door is unlocked and can be opened by clicking on it
    private bool doorUnlocked;
    //indicates if the door is currently open so that the player can go through
    private bool doorOpen;

    //from Parent:
    //Awake: disable Collider
    //Start: get interactable Component
    //OnMouseOver: Icon
    //OnMouseDown: MangeInteractables()

    protected override void OnMouseEnter()
    {
        if (!GameManager.gameManager.GameIsOnPause() &&
            !GameManager.gameManager.InspectorOpen() &&
            !GameManager.gameManager.JournalOpen())
        {
            if (!doorOpen)
            {
                //var hasPrerequ = GetComponent<Prerequisite>();
                if (interactable != null)
                {
                    interactable.ShowInfo(AllPrerequsComplete());
                }
            }
            
        }
    }

    public override void ManageInteractables()
    {
        //make item interactable, if prerequisite is met
        if (interactable != null)
        {
            Reference.instance.currentDoor = this;

            if (AllPrerequsComplete())
            {
                ManageJournalInfo();

                Debug.Log("I'm opening because I was unlocked!");
                interactable.enabled = true;
                //CheckForCollectable();
                interactable.Interact();
            }
            else
            {
                Debug.Log("I'm locked, sorry");
            }
        }
    }

    public void UnlockDoor()
    {
        doorUnlocked = true;
    }

    public bool DoorIsUnlocked()
    {
        return doorUnlocked;
    }

    public void OpenDoor()
    {
        doorOpen = true;
    }

    public void CloseDoor()
    {
        doorOpen = false;
    }

    public bool DoorIsOpen()
    {
        return doorOpen;
    }
}
