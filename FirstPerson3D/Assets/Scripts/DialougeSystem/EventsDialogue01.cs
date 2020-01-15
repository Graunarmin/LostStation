using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create Logic for events here!
//use References to acces GameObjects

[CreateAssetMenu(fileName = "New Event", menuName = "Event")]
public class EventsDialogue01 : ScriptableObject
{

    public void AddJournalPage(JournalPage jp)
    {
        Reference.instance.journalManager.UpdateJournal(jp);
    }

    public void SetAmAlice()
    {
        Debug.Log("You chose 'Alice'");
        DialogueTracker.dialogueTracker.start = false;

        DialogueTracker.dialogueTracker.amAlice = true;
        DialogueTracker.dialogueTracker.willHelp = false;
        DialogueTracker.dialogueTracker.wontHelp = false;

        ResetNobody();
    }

    public void ResetAlice()
    {
        DialogueTracker.dialogueTracker.amAlice = false;
        DialogueTracker.dialogueTracker.willHelp = false;
        DialogueTracker.dialogueTracker.wontHelp = false;
    }

    public void SetWillHelp()
    {
        Debug.Log("You chose to help");
        DialogueTracker.dialogueTracker.willHelp = true;

    }

    public void SetWontHelp()
    {
        Debug.Log("You chose not to help");
        DialogueTracker.dialogueTracker.wontHelp = true;

    }

    public void SetAmNobody()
    {
        Debug.Log("You chose 'Nobody'");
        DialogueTracker.dialogueTracker.start = false;

        DialogueTracker.dialogueTracker.amNobody = true;
        DialogueTracker.dialogueTracker.comeBack = false;

        ResetAlice();
    }

    public void ResetNobody()
    {
        DialogueTracker.dialogueTracker.amNobody = false;
        DialogueTracker.dialogueTracker.comeBack = false;
    }

    public void SetComeBack()
    {
        DialogueTracker.dialogueTracker.comeBack = true;
        DialogueTracker.dialogueTracker.amNobody = false;
        ResetAlice();
        
    }
}
