using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCanvas : MonoBehaviour
{


    public void Activate()
    {
        GameManager.gameManager.SwitchCameras("2D");
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }


    public void Close()
    {
        DialogueManager.instance.dialogueBoxUI.SetActive(false);
        DialogueManager.instance.dialogueOptionUI.SetActive(false);
        DialogueManager.instance.isDialogueOption = false;
        DialogueManager.instance.inDialogue = false;
        gameObject.SetActive(false);
        DialogueManager.instance.FireOnDialogueClosed();
        //time is reset in game manager
        GameManager.gameManager.SwitchCameras("3D");
    }
}