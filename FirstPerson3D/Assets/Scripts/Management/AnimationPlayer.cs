using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AnimationPlayer
{
    public Animator animator;
    public string animationName;
    public ItemAsset requiredItem;
    public bool playOnlyOnce;
    public bool alreadyPlayed;

    public void PlayAnimation()
    {
        //if the animation was not already played
        if (!alreadyPlayed)
        {
            //if we need an item to see the animation
            //if (requiredItem != null)
            //{
                ////do we have the item?
                //if (InventoryManager.invManager.ContainerContainsItem(requiredItem))
                //{
                //    Debug.Log("Item for Animation is in Backpack");
                    animator.Play(animationName);
                    if (playOnlyOnce)
                    {
                        SetAlreadyPlayed();
                    }
            //    }
            //    else
            //    {
            //        Debug.Log("Item for Animation not in Backpack");
            //    }
            //}
            else
            {
                animator.Play(animationName);
            }

        }
    }


    public void SetAlreadyPlayed()
    {
        alreadyPlayed = true;
    }
}
