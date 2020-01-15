using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTracker : MonoBehaviour
{
    #region singleton
    public static DialogueTracker dialogueTracker;

    private void Awake()
    {
        if (dialogueTracker == null)
        {
            dialogueTracker = this;
        }

    }
    #endregion

    #region bools

    public bool start = true;
    public bool amAlice;
    public bool willHelp;
    public bool wontHelp;
    public bool amNobody;
    public bool comeBack;
    public bool foundOut;

    #endregion

    #region dialogues

    public DialogueBase whoAreYou;
    public DialogueBase willYouHelp;
    public DialogueBase foundOutWhoYouAre;

    #endregion

    public DialogueBase currentDialogue;
}
