using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueCanvas : MonoBehaviour
{


    public void Activate()
    {
        GameManager.gameManager.SwitchCameras("2D");
        gameObject.SetActive(true);
    }


    public void Close()
    {
        DialogueManager.instance.dialogueBoxUI.SetActive(false);
        DialogueManager.instance.dialogueOptionUI.SetActive(false);
        DialogueManager.instance.isDialogueOption = false;
        DialogueManager.instance.inDialogue = false;
        GameManager.gameManager.SwitchCameras("3D");
        gameObject.SetActive(false);
    }
}