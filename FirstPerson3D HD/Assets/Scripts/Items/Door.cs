using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{
    //the keypad that unlocks the door (if any)
    public Keypad connectedKeypad;

    //indicates if the door is unlocked and can be opened by clicking on it
    [SerializeField] bool doorUnlocked;
    //if door is only temporarily closed (e.g.elevator)
    [SerializeField] bool doorBlocked;
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

            if (AllPrerequsComplete() && !doorBlocked)
            {
                ManageJournalInfo();

                Debug.Log("I'm opening because I was unlocked!");
                interactable.enabled = true;
                //CheckForCollectable();
                interactable.Interact();
                //AudioManager.audioManager.PlaySound(AudioManager.audioManager.doorOpen);
            }
            else
            {
                Debug.Log("I'm locked, sorry");
                //AudioManager.audioManager.PlaySound(AudioManager.audioManager.doorLocked);
            }
        }
    }

    public void UnlockDoor()
    {
        doorUnlocked = true;
    }

    public void LockDoor()
    {
        doorUnlocked = false;
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

    //Prevent doors from being opened even though they are unlocked,
    //e.g. while elevator is moving
    public void BlockDoor()
    {
        doorBlocked = true;
    }

    public void UnblockDoor()
    {
        doorBlocked = false;
    }

    public bool DoorIsAccessible()
    {
        return doorBlocked;
    }
        
}
