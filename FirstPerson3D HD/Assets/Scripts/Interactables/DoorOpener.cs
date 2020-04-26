using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DoorOpener : Interactable
{
    public List<Animator> animators = new List<Animator>();
    public List<string> openingAnimations;
    public List<string> closingAnimations;
    public GameObject OpenSound;
    public GameObject CloseSound;

    public Collider doorLock;
    private Door door;

    private void Awake()
    {
        door = GetComponent<Door>();
    }

    public override void Interact()
    {
        OpenDoor();
    }

    public void OpenDoor()
    {
        //Debug.Log("Opening this door " + name);
        //doorAnimation.Play(openAnimationName);
        if (OpenSound != null)
        {
            OpenSound.SetActive(false);
        }

        if (animators.Count == 0)
        {
            //just a workaround in case animation is missing
            //careful: if deacivated it can't be closed again!
            gameObject.SetActive(false);
        }
        else
        {
            for (int i = 0; i < animators.Count; i++)
            {
                animators[i].Play(openingAnimations[i]);
                if(OpenSound != null)
                {
                    OpenSound.SetActive(true);
                }
            }
        }


        if (doorLock != null)
        {
           StartCoroutine(EnableDoorCollider(false));
        }
        door.OpenDoor();
    }

    public override void ShowInfo(bool allPrerequsComplete)
    {

        if (allPrerequsComplete)
        {
            if (!door.DoorIsOpen())
            {
                if (interactionIcon != null)
                {
                    interactionIcon.gameObject.SetActive(true);
                }
            }

        }
        else
        {
            //Debug.Log("not all prerequs complete");
        }
    }

    public void CloseDoor()
    {
        if (CloseSound != null)
        {
            CloseSound.SetActive(false);
        }
        //doorAnimation.Play(closeAnimationName);
        for (int i = 0; i < animators.Count; i++)
        {
            animators[i].Play(closingAnimations[i]);
            if(CloseSound != null)
            {
                CloseSound.SetActive(true);
            }

        }
        if (doorLock != null)
        {
            StartCoroutine(EnableDoorCollider(true));
        }
        door.CloseDoor();
    }

    private IEnumerator EnableDoorCollider(bool enable)
    {
        yield return new WaitForSeconds(1.5f);
        doorLock.enabled = enable;
    }

}
