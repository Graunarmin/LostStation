using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationStarter : Interactable
{
    public DialogueBase dialogue;
    public DialogueCanvas diaCanvas;

    public override void Interact()
    {
        //inDialogue = true
        //currentlySpeaking = true
        //DialogueManager.instance.EnqueueDialogue(dialogue);
        diaCanvas.Activate();

        //based on what happened so far, choose the next dialogue
        DialogueManager.instance.EnqueueDialogue(
            DialogueTracker.dialogueTracker.ChooseDialogue());
    }
}
