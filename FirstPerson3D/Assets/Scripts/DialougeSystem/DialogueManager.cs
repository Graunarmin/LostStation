using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{

    #region singleton
    public static DialogueManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //make sure both boxes are switched off
        dialogueBoxUI.gameObject.SetActive(false);
        dialogueOptionUI.gameObject.SetActive(false);
    }
    #endregion

    #region basic member
    public GameObject dialogueBoxUI;
    public Image dialoguePortrait;
    public TextMeshProUGUI dialogueName;
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.005f;

    public Queue<DialogueBase.Info> dialogueInfo = new Queue<DialogueBase.Info>();
    #endregion

    #region option member
    public GameObject dialogueOptionUI;
    public Image optionsPortrait;
    public TextMeshProUGUI optionsName;
    public TextMeshProUGUI questionText;
    public GameObject[] optionButtons;

    public bool inDialogue;
    [HideInInspector]
    public bool isDialogueOption;
    private bool isCurrentlyTyping;
    private int optionsAmount;
    private string completeText;
    #endregion

    #region basic dialogue

    public void EnqueueDialogue(DialogueBase db)
    {
        //if dialogue has already begun: do nothing
        if (inDialogue)
        {
            return;
        }
        //else enqueue the stuff and set inDialogue = true
        else
        {
            inDialogue = true;
            DialogueTracker.dialogueTracker.currentDialogue = db;
        }

        //activate the Canvas
        //Reference.instance.dialogueCanvas.gameObject.SetActive(true);
        //activate dialogue Box for basic dialogue;
        dialogueBoxUI.SetActive(true);

        //clear out old queue
        dialogueInfo.Clear();

        //check if dialogue contains options and handle them
        OptionsParser(db);

        //enqueue all text from the dialogue
        foreach (DialogueBase.Info info in db.dialogueInfo)
        {
            dialogueInfo.Enqueue(info);
        }

        //show dialogue in textfield
        DequeueDialogue();
    }

    public void DequeueDialogue()
    {
        //autocomplete text on click 
        if (isCurrentlyTyping)
        {
            CompleteText();
            StopAllCoroutines();
            isCurrentlyTyping = false;
            return;
        }

        //if we have no more dialogue: return
        if (dialogueInfo.Count == 0)
        {
            EndOfDialogue();
            return;
        }

        DialogueBase.Info info = dialogueInfo.Dequeue();
        completeText = info.myText;

        dialogueText.text = info.myText;

        dialoguePortrait.sprite = info.character.myPortrait;
        dialogueName.text = info.character.myName;


        dialogueText.text = "";
        StartCoroutine(TypeText(info));
    }

    IEnumerator TypeText(DialogueBase.Info info)
    {
        isCurrentlyTyping = true;

        foreach (char c in info.myText.ToCharArray())
        {
            yield return new WaitForSeconds(typingSpeed);
            dialogueText.text += c;
        }

        isCurrentlyTyping = false;
    }

    private void CompleteText()
    {
        dialogueText.text = completeText;
    }

    public void EndOfDialogue()
    {
        dialogueBoxUI.SetActive(false);
        //if there are any Options: open them at the end
        OpenOptions();
    }

    #endregion


    #region dialogue with options

    private void OpenOptions()
    {
        //check if there are options and open them if
        if (isDialogueOption)
        {
            dialogueOptionUI.SetActive(true);
        }
        else
        {
            inDialogue = false;
            AddJournalPage();
            Reference.instance.dialogueCanvas.gameObject.SetActive(false);
            GameManager.gameManager.SwitchCameras("3D");

        }
    }

    public void closeOptions()
    {
        dialogueOptionUI.SetActive(false);
    }

    private void OptionsParser(DialogueBase db)
    {
        //checks the type of db
        if (db is DialogueOptions)
        {

            isDialogueOption = true;

            DialogueOptions dialogueOptions = db as DialogueOptions;

            optionsAmount = dialogueOptions.optionsInfo.Length;
            questionText.text = dialogueOptions.questionText;
            optionsPortrait.sprite = dialogueOptions.character.myPortrait;
            optionsName.text = dialogueOptions.character.myName;

            //first turn off all "old buttons
            for (int i = 0; i < optionButtons.Length; i++)
            {
                optionButtons[i].SetActive(false);
            }

            //only turn on as many option buttons as needed
            for (int i = 0; i < optionsAmount; i++)
            {
                optionButtons[i].SetActive(true);
                //set Text on Button
                optionButtons[i].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = dialogueOptions.optionsInfo[i].buttonName;

                //add possible Events to Buttons
                UnityEventHandler myEventHandler = optionButtons[i].GetComponent<UnityEventHandler>();
                myEventHandler.eventHandler = dialogueOptions.optionsInfo[i].myEvent;

                //check if there is a follow-up dialogue and open it if there is
                if (dialogueOptions.optionsInfo[i].nextDialogue != null)
                {
                    myEventHandler.myDialogue = dialogueOptions.optionsInfo[i].nextDialogue;
                }
                else
                {
                    myEventHandler.myDialogue = null;
                }
            }
        }
        else
        {
            isDialogueOption = false;
        }
    }

    private void AddJournalPage()
    {
        if (DialogueTracker.dialogueTracker.currentDialogue.journalPage != null)
        {
            Reference.instance.journalManager.UpdateJournal(DialogueTracker.dialogueTracker.currentDialogue.journalPage);
        }
        DialogueTracker.dialogueTracker.currentDialogue = null;
    }

    #endregion
}