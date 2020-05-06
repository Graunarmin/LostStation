using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyPadInspector : Interactable
{
    public Keypad keypad;
    public CorrectSolutionReaction reaction;

    [HideInInspector]
    public Coroutine checkPassword;

    public override void Interact()
    {
        if (!keypad.door.DoorIsUnlocked()){

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
            => keypad.PasswordCorrect());

        yield return new WaitForSecondsRealtime(1);

        //unlock the door
        keypad.door.UnlockDoor();
        GetComponent<Collider>().enabled = false;

        //React in some way
        if (reaction != null)
        {
            reaction.React();
        }

        //and close the Keypad
        Reference.instance.keyPad.Close();
    }

}
