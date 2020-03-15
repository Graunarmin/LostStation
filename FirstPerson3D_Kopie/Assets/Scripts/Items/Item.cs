using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [HideInInspector]
    public Collider col;
    [HideInInspector]
    public Interactable interactable;

    public ItemAsset itemInfo;

    //delete, contained in ItemAsset!
    public JournalPage journalPage;
    //maybe shift over to ItemAsset as well?
    public Region location;

    //is always called (when Object is active)
    void Awake()
    {
        //as long as an item has a collider, it is going to put it in here, otherwise = null
        if (GetComponent<Collider>() != null)
        {
            col = GetComponent<Collider>();
            //all colliders are disabled in the beginning
            col.enabled = false;
        }
        else
        {
            col = null;
        }
    }

    //is called when Script is enabled
    private void Start()
    {
        //If that Prop has an interactable Component it will put it in "interactable", otherwise it's just null
        interactable = GetComponent<Interactable>();
    }

    protected virtual void OnMouseEnter()
    {
        if (!GameManager.gameManager.CurrentlyInteracting())
        {
            if (interactable != null)
            {
                //change Cursor
                interactable.ShowInfo(AllPrerequsComplete());
            }
        }
    }


    private void OnMouseDown()
    {
        //if game not on pause && if neither 2D nor 3D inspector open
        if (!GameManager.gameManager.CurrentlyInteracting())
        {
            //Set the current Item to the clicked one
            Reference.instance.currentItem = this;
            Debug.Log("Current Item: " + name);

            ManageInteractables();

            //update Info about Object

            //We don't need Info on a door thats opening and just sliding under our mouse (works)
            //And neither do we need to see the speech-bubble while talking (!!FIX!!)
            if ((this is Door && ((Door)this).DoorIsOpen())
                || gameObject.GetComponent<ConversationStarter>() != null)
            {
                //or hide info when using icons
                interactable.HideInfo();
            }
            else
            {
                if (interactable != null && gameObject.activeInHierarchy)
                {
                    interactable.ShowInfo(AllPrerequsComplete());
                }
            }
        }
    }

    public virtual void ManageInteractables()
    {

        //make item interactable, if prerequisite is met
        if (interactable != null)
        {
            if (AllPrerequsComplete())
            {
                ManageJournalInfo();
                interactable.enabled = true;
                interactable.Interact();
            }
        }
    }

    public virtual void ManageJournalInfo()
    {
        if(itemInfo != null)
        {
            if (itemInfo.journalPage != null)
            {
                GameManager.gameManager.FireNewJournalEntry(itemInfo.journalPage);
            }
        }
    }


    void OnMouseExit()
    {
        //no longer Highlight Item if interactable
        if (interactable != null)
        {
            //change cursor back
            interactable.HideInfo();
        }


        if (!GameManager.gameManager.CurrentlyInteracting())
        {

            //set current Item to null
            Reference.instance.currentItem = null;
            if(Reference.instance.currentKeypad != null)
            {
                Reference.instance.currentKeypad.GetComponent<KeyPadInspector>().StopCoroutine(
                    Reference.instance.currentKeypad.GetComponent<KeyPadInspector>().checkPassword);
                Reference.instance.currentKeypad = null;
            }
            //Debug.Log("current item is null");
        }
    }

    public bool AllPrerequsComplete()
    {
        var prerequisites = gameObject.GetComponents<Prerequisite>();

        //either there is none - in which case it's "complete"
        if(prerequisites.Length == 0)
        {
            //Debug.Log("No Prerequisites");
            return true;
        }

        //or we have to test each prerequ and as soon as one is not met, it's incomplete
        foreach (Prerequisite p in prerequisites)
        {
            if (!p.Complete)
            {
                return false;
            }
        }
        return true;
    }
}
