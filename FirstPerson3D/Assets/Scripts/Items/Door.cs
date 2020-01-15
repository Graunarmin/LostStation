using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Item
{

    public bool doorUnlocked;
    public Keypad connectedKeypad;
    public bool doorOpen;

    //from Parent:
    //Awake: disable Collider
    //Start: get interactable Component
    //OnMouseOver: Highlighten
    //OnMouseDown: MangeInteractables()

    protected override void OnMouseEnter()
    {
        if (!GameManager.gameManager.GameIsOnPause() &&
            !GameManager.gameManager.InspectorOpen() &&
            !GameManager.gameManager.JournalOpen())
        {
            if (!doorOpen)
            {
                var hasPrerequ = GetComponent<Prerequisite>();
                if (interactable != null)
                {
                    interactable.ShowInfo(hasPrerequ);
                }
            }
            
        }
    }

    public override void ManageInteractables()
    {

        var hasPrerequ = GetComponent<Prerequisite>();
        //make item interactable, if prerequisite is met
        if (interactable != null)
        {
            Reference.instance.currentDoor = this;

            if (!hasPrerequ || (hasPrerequ && hasPrerequ.Complete))
            {

                Debug.Log("I'm opening because I was unlocked!");
                interactable.enabled = true;
                CheckForCollectable();
                interactable.Interact();
            }
            else
            {
                Debug.Log("I'm locked, sorry");
            }
        }
    }
}
