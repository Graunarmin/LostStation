using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConversationStarter : Interactable
{
    //public List<DialogueBase> dialogues;
    public DialogueBase dialogue;
    public DialogueCanvas diaCanvas;

    public override void Interact()
    {
        //inDialogue = true
        //currentlySpeaking = true
        //DialogueManager.instance.EnqueueDialogue(dialogue);
        diaCanvas.Activate();
        DialogueManager.instance.EnqueueDialogue(chooseDialogue());
    }

    private DialogueBase chooseDialogue()
    {
        if (DialogueTracker.dialogueTracker.start)
        {
            return DialogueTracker.dialogueTracker.whoAreYou;
        }
        if(DialogueTracker.dialogueTracker.amAlice &&
            DialogueTracker.dialogueTracker.wontHelp)
        {
            return DialogueTracker.dialogueTracker.willYouHelp;
        }
        if (DialogueTracker.dialogueTracker.comeBack)
        {
            return DialogueTracker.dialogueTracker.foundOutWhoYouAre;
        }


        return DialogueTracker.dialogueTracker.whoAreYou;
    }
}
