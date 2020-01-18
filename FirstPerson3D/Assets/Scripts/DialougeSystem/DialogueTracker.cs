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

    public bool start = true;
    public DialogueBase currentDialogue;


    #region Dialogue 01
    public DialogueBase whoAreYou;
    public bool amAlice;
    public bool amNobody;
    public bool comeBack;

    public DialogueBase willYouHelp;
    public bool willHelp;
    public bool wontHelp;
    #endregion

    #region Dialogue 02
    public DialogueBase foundOutWhoYouAre;
    #endregion

    #region Dialogue 03
    public DialogueBase tooDark;
    public bool gotFlashlight;
    #endregion


    public DialogueBase ChooseDialogue()
    {
        if (start)
        {
            return whoAreYou;
        }
        if (amAlice && wontHelp)
        {
            return willYouHelp;
        }
        if (comeBack)
        {
            return foundOutWhoYouAre;
        }
        if(amAlice && willHelp)
        {
            return tooDark;
        }


        return whoAreYou;
    }

}
