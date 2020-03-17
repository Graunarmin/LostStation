using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElevatorControlsCanvas : MonoBehaviour, IPuzzleCanvas
{
    [SerializeField] Button upButton;
    [SerializeField] Button downButton;
    [SerializeField] Button talkButton;
    [SerializeField] Button controlsButton;
    [SerializeField] ElevatorPuzzleCanvas circuit;
    [SerializeField] DialogueCanvas diaCanvas;


    private bool upButtonState;
    private bool downButtonState;
    private bool controlsButtonState;

    private void Awake()
    {
        upButton.interactable = false;
        downButton.interactable = false;
        talkButton.interactable = true;
        controlsButton.interactable = true;
        DialogueManager.OnDialogueClosed += ReactivateAfterDialogue;
    }

    public void Activate()
    {
        GameManager.gameManager.SwitchCameras("2D");
        gameObject.SetActive(true);
        Time.timeScale = 0f;

    }

    public void ReactivateAfterCircuit()
    {
        gameObject.SetActive(true);
        if (circuit.circuitSet)
        {
            controlsButton.interactable = false;
            ActivateUp();

        }
    }
    public void ReactivateAfterDialogue()
    {
        ResetButtons();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1f;
        GameManager.gameManager.SwitchCameras("3D");
    }

    

    public void TalkButton()
    {
        //deactivate all buttons in the background
        //so wo don't accidentally click on them
        SaveButtonStates();
        DisableAllButtons();

        //activate dialogue canvas
        diaCanvas.Activate();
        //start Conversation with Robot
        DialogueManager.instance.EnqueueDialogue(
            DialogueTracker.dialogueTracker.ChooseTalkButtonDialogue());
    }

    private void SaveButtonStates()
    {
        upButtonState = upButton.interactable;
        downButtonState = downButton.interactable;
        controlsButtonState = controlsButton.interactable;
    }

    private void DisableAllButtons()
    {
        upButton.interactable = false;
        downButton.interactable = false;
        talkButton.interactable = false;
        controlsButton.interactable = false;
    }

    private void ResetButtons()
    {
        upButton.interactable = upButtonState;
        downButton.interactable = downButtonState;
        controlsButton.interactable = controlsButtonState;
        talkButton.interactable = true;
    }

    public void UpButton()
    {
        AudioManager.audioManager.PlaySound(AudioManager.audioManager.elevatorButtons);
        ActivateDown();
        ElevatorManager.elevator.Up();
        Close();

    }

    public void DownButton()
    {
        ActivateUp();
        ElevatorManager.elevator.Down();
        Close();
    }

    

    private void ActivateUp()
    {
        upButton.interactable = true;
        downButton.interactable = false;

    }

    private void ActivateDown()
    {
        upButton.interactable = false;
        downButton.interactable = true;
    }

    public void ControlsButton()
    {
        //close this canvas so there is no confusion when clicking
        gameObject.SetActive(false);
        //open circuit Canvas
        circuit.Activate();
        
    }


}
