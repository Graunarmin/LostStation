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

        //when the keycard is collected we get notified here
        InventoryManager.OnKeyCardCollected += SetKeyCardCollected;

    }
    #endregion

    public bool start = true;
    public DialogueBase currentDialogue;


    #region Dialogue 01
    public bool endFirstDialogue;

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

    #region Dialogue 04
    public DialogueBase notMoving;
    public bool endSecurityDialogue;
    #endregion

    #region Dialogue05
    public DialogueBase tookYouLongEngough;
    public DialogueBase tookYouLongEnoughWOCard;
    public DialogueBase beenHereWCard;
    public DialogueBase beenHereWOCard;

    public bool keyCardCollected;
    public bool haveBeenHereBefore;
    public bool notBob;
    public bool accessGranted;


    public void SetKeyCardCollected()
    {
        //Debug.Log("I noticed that Keycard was collected");
        keyCardCollected = true;
    }
    #endregion

    #region Dialogue06
    public DialogueBase helloAlice;

    #endregion

    #region Dialogue07
    public DialogueBase helloBob;

    #endregion


    public DialogueBase ChooseDialogue()
    {
        if (!endFirstDialogue)
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
            if (amAlice && willHelp)
            {
                endFirstDialogue = true;
                return tooDark;
            }
        }else if (!endSecurityDialogue)
        {
            if (gotFlashlight && !GameManager.gameManager.powerIsBack)
            {
                return notMoving;
            }

            //if we did not yet answer the security questions correctly
            else if(gotFlashlight && GameManager.gameManager.powerIsBack && !accessGranted)
            {
                //if keycard collected and not talked about this before
                if (keyCardCollected && !haveBeenHereBefore)
                {
                    if (notBob)
                    {
                        return helloAlice;
                    }

                    return tookYouLongEngough;
                }
                //if keycard collected and talked about this before
                else if (keyCardCollected && haveBeenHereBefore)
                {
                    if (notBob)
                    {
                        return helloAlice;
                    }

                    return beenHereWCard;
                }
                //if not keycard collected and not talked about this before
                else if (!keyCardCollected && !haveBeenHereBefore)
                {
                    return tookYouLongEnoughWOCard;
                }
                //if not keycard collected and talked about this before
                else if (!keyCardCollected && haveBeenHereBefore)
                {
                    return beenHereWOCard;
                }
            }

            //if we answered all questions correctly
            if (accessGranted)
            {
                return helloBob;
            }
        }
        return whoAreYou;
    }

    public DialogueBase ChooseTalkButtonDialogue()
    {
        return null;
    }

}
