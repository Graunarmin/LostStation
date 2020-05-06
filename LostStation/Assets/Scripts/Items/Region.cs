using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Region : MonoBehaviour
{
    
    public List<Item> containedItems = new List<Item>();
    //so the contained doors can be closed if we leave the region
    public List<Door> containedDoors = new List<Door>();

    //animations that are to be played when region is entered
    [SerializeField] List<AnimationPlayer> animations;
    [SerializeField] List<CorrectSolutionReaction> reactions;

    public Collider wallCollider;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hi, I'm entering the " + name);

        EnterRegion();
    }

    public void EnterRegion()
    {
        //add Region to List of current Regions
        Reference.instance.currentRegions.Add(this);

        //switch on colliders of all contained Items
        SetContainedItems(true);

        if(animations.Count > 0)
        {
            PlayAnimationsEnter();
        }

        if(reactions.Count > 0)
        {
            foreach(CorrectSolutionReaction r in reactions)
            {
                r.React();
            }
        }

        if(wallCollider != null)
        {
            wallCollider.gameObject.SetActive(true);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        Debug.Log("And now I'm leaving the " + name);

        ExitRegion();
    }

    public void ExitRegion()
    {
        //set current Region to null
        Reference.instance.currentRegions.Remove(this);

        CloseDoors();
        //switch off colliders of all contained Items
        SetContainedItems(false);

        if (wallCollider != null)
        {
            wallCollider.gameObject.SetActive(false);
        }
    }


    public void SetContainedItems(bool colliderValue)
    {
        if(containedItems.Count > 0)
        {
            foreach (Item item in containedItems)
            {
                var prerequisites = GetComponents<Prerequisite>();
                //if item has a collider:
                if (item.col != null)
                {
                    bool itemAccess = false;
                    foreach(Prerequisite p in prerequisites)
                    {
                        //if it has a Prerequisite which is about acces
                        if (p.itemAccess)
                        {
                            itemAccess = true;
                            //the prerequisite has to be met to enable the collider
                            if (p.Complete)
                            {
                                item.col.enabled = colliderValue;
                                //Debug.Log("ItemAccess granted");
                                return;
                            }
                        }
                        
                    }
                    if(!itemAccess)
                    {
                        //turn collider on or off (depending in entering or exiting the region
                        item.col.enabled = colliderValue;
                    }
                }
            }
        }
        
    }

    public void RemoveItem(Item item)
    {
        containedItems.Remove(item);

        if(item is Door)
        {
            containedDoors.Remove((Door)item);
        }
    }

    private void CloseDoors()
    {
        foreach(Door door in containedDoors)
        {
            if(door.gameObject.GetComponent<DoorOpener>() != null && door.DoorIsOpen())
            {
                door.gameObject.GetComponent<DoorOpener>().CloseDoor();
            }
        }
    }

    private void PlayAnimationsEnter()
    {
        if(animations.Count > 0)
        {
            if (AllPrerequsComplete())
            {
                foreach (AnimationPlayer animation in animations)
                {
                    animation.PlayAnimation();
                }
            }
        }
        
    }

    public bool AllPrerequsComplete()
    {
        var prerequisites = gameObject.GetComponents<Prerequisite>();

        //either there is none - in which case it's "complete"
        if (prerequisites.Length == 0)
        {
            //Debug.Log("No Prerequisites");
            return true;
        }

        //or we have to test each prerequ and as soon as one is not met, it's incomplete
        foreach (Prerequisite p in prerequisites)
        {
            p.Print();
            if (!p.Complete)
            {
                return false;
            }
        }
        return true;
    }
}
