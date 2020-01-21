using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPadInspector : Interactable
{
    public string infoTextPWCorrect;
    public Coroutine checkPassword;

    public override void Interact()
    {
        if (!Reference.instance.currentKeypad.door.doorUnlocked){

            Reference.instance.keyPad.Activate();

            //Test if this is the first ever Canvas that opens and if so, show Tutorial on how to clos
            TutorialManager.tutorialManager.FirstCanvas();

            checkPassword = StartCoroutine(CheckPassword());
        }
    }

    public IEnumerator CheckPassword()
    {
        //wait until the password of the current Keypad is correct
        yield return new WaitUntil(()
            => Reference.instance.currentKeypad.passwordCorrect);

        yield return new WaitForSecondsRealtime(1);
        //unlock the door
        Reference.instance.currentKeypad.door.doorUnlocked = true;
        GetComponent<Collider>().enabled = false;
        //and close the Keypad
        Reference.instance.keyPad.Close();
    }

    //public override void ShowInfo(Prerequisite hasPrereq)
    //{
    //    //EnableInfoCanvas();

    //    //is not clickable bc. Prerequ is not met
    //    if (hasPrereq && !hasPrereq.Complete)
    //    {
    //        if (infoTextInactive != "")
    //        {
    //            displayText.text = infoTextInactive;
    //        }
    //    }
    //    //is clickable bc. Prerequ is met or it has none
    //    else if (!hasPrereq || hasPrereq && hasPrereq.Complete)
    //    {
    //        //correct password was already enterd
    //        if (GetComponent<Keypad>().passwordCorrect)
    //        {
    //            if (infoTextActive != "")
    //            {
    //                displayText.text = infoTextPWCorrect;
    //            }
    //        }
    //        //Correct password was not enterd yet
    //        else
    //        {
    //            if (infoTextActive != "")
    //            {
    //                displayText.text = infoTextActive;
    //            }
    //        }

    //    }
    //}

}
