using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UnityEventHandler : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent eventHandler;
    public DialogueBase myDialogue;

    //this is what happens when you click on a button
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        eventHandler.Invoke();

        //close the options
        DialogueManager.instance.CloseOptions();
        DialogueManager.instance.inDialogue = false;

        //and show follow-up dialogue (if there is any)
        if(myDialogue != null)
        {
            DialogueManager.instance.EnqueueDialogue(myDialogue);
        }
        else
        {
            DialogueManager.instance.CloseDialogue();

        }
    }
}
