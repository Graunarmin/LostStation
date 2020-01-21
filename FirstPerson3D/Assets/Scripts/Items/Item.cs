﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{

    [HideInInspector]
    public Collider col;
    [HideInInspector]
    public Interactable interactable;

    public ItemAsset itemInfo;

    public JournalPage journalPage;
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
        ////and if it updates the jorunal we put the info in here:
        //journalInfo = GetComponent<JournalPage>();
    }

    protected virtual void OnMouseEnter()
    {
        if (!GameManager.gameManager.CurrentlyInteracting())
        {
            //var hasPrerequ = GetComponent<Prerequisite>();

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

            ManageJournalInfo();

            //update Info about Object

            //We don't need Info on a door thats opening and just sliding under our mouse (works)
            //And neither do we need to see the speech-bubble while talking (!!FIX!!)
            if ((this is Door && ((Door)this).doorOpen)
                || gameObject.GetComponent<ConversationStarter>() != null)
            {
                //do nothing

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

    public virtual void ManageJournalInfo()
    {
        if (journalPage != null)
        {
            GameManager.gameManager.FireNewJournalEntry(journalPage);
        }
    }

    public virtual void ManageInteractables()
    {

        //make item interactable, if prerequisite is met
        if (interactable != null)
        {
            if (AllPrerequsComplete())
            {
                interactable.enabled = true;
                //CheckForCollectable();
                interactable.Interact();
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
            //GetComponentInChildren<Renderer>().material.color = new Color32(255, 255, 255, 255);
        }


        if (!GameManager.gameManager.CurrentlyInteracting())
        {

            //set current Item to null
            Reference.instance.currentItem = null;
            //Reference.instance.currentKeypad = null;
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

        //foreach(Prerequisite p in prerequisites)
        //{
        //    Debug.Log("There are Prerequs: ");
        //    p.Print();
        //}

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

    //Check if the Item required a collectable 
    //protected void CheckForCollectable()
    //{
    //    // if it was required to hold a certain collectable:
    //    // dismiss this collectable now and close Inventory Display

    //    var prerequisites = GetComponents<Prerequisite>();

    //    foreach(Prerequisite p in prerequisites)
    //    {
    //        if(p is CollectPrereq)
    //        {
    //            Reference.instance.collectHeld = null;
    //            return;
    //        }
    //    }

    //}
}
