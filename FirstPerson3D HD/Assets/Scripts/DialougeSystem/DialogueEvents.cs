using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create Logic for events here!
//use References to acces GameObjects

[CreateAssetMenu(fileName = "New DialogueEvent", menuName = "DialogueEvent")]
public class DialogueEvents : ScriptableObject
{

    public void AddJournalPage(JournalPage jp)
    {
        JournalManager.journalManager.UpdateJournal(jp);
    }

    #region Dialogue  01
    public void SetAmAlice()
    {
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
        DialogueTracker.dialogueTracker.willHelp = true;
        DialogueTracker.dialogueTracker.wontHelp = false;
    }

    public void SetWontHelp()
    {
        DialogueTracker.dialogueTracker.wontHelp = true;
    }

    public void SetAmNobody()
    {
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
    #endregion

    #region Dialogue 03

    public void SetFlashlight()
    {
        DialogueTracker.dialogueTracker.gotFlashlight = true;
        DialogueTracker.dialogueTracker.endFirstDialogue = true;
        //GameManager.gameManager.flashlightEnabled = true;
        Reference.instance.flashlight.ReceiveFlashlight();
        TutorialManager.tutorialManager.StartCoroutine(TutorialManager.tutorialManager.WaitForEndOfDialogue());
        Debug.Log("Moving on");
    }

    #endregion



    #region Dialogue 05

    public void SetHaveBeenHereBefore()
    {
        DialogueTracker.dialogueTracker.haveBeenHereBefore = true;
    }

    public void SetNotBob()
    {
        DialogueTracker.dialogueTracker.notBob = true;
    }

    public void ResetNotBob()
    {
        DialogueTracker.dialogueTracker.notBob = false;
    }

    public void SetAccessGranted()
    {
        DialogueTracker.dialogueTracker.accessGranted = true;
    }

    #endregion

    #region Intercom Dialogue

    public void SetHelpedOnce()
    {
        DialogueTracker.dialogueTracker.helpedOnce = true;
    }

    public void SetHelpedTwice()
    {
        DialogueTracker.dialogueTracker.helpedTwice = true;
    }

    #endregion
}
