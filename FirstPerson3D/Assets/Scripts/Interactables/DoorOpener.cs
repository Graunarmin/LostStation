using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorOpener : Interactable
{
    public Animator doorAnimation;
    public string openAnimationName;
    public string closeAnimationName;
    private Door door;

    private void Awake()
    {
        door = GetComponent<Door>();
    }

    public override void Interact()
    {
        Debug.Log("Opening this door " + name);
        doorAnimation.Play(openAnimationName);
        door.doorOpen = true;
    }

    public override void ShowInfo(Prerequisite hasPrereq)
    {
        if (!hasPrereq || (hasPrereq && hasPrereq.Complete))
        {
            if (!door.doorOpen)
            {
                if (interactionIcon != null)
                {
                    interactionIcon.gameObject.SetActive(true);
                }
            }
            
        }
    }
}
