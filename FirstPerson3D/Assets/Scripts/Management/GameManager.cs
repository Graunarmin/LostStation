using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    #region singleton
    public static GameManager gameManager;

    //very bad singleton, look up how it's done properly!!
    private void Awake()
    {

        if (gameManager == null)
        {
            gameManager = this;
        }

    }
    #endregion

    #region userinput

    private void Update(){
        //on rightclick: close imageviewer/ oberserver cam / Keypad / Puzzle / DrawingPanel
        if (Input.GetMouseButtonDown(1) && Reference.instance.currentItem != null)
        {
            if (!JournalOpen())
            {
                if (Reference.instance.ivCanvas.gameObject.activeInHierarchy)
                {
                    Reference.instance.ivCanvas.Close();
                }
                if (Reference.instance.obsCam.gameObject.activeInHierarchy)
                {
                    Reference.instance.obsCam.Close();
                }
                if (Reference.instance.keyPad.gameObject.activeInHierarchy)
                {
                    Reference.instance.keyPad.Close();
                    //StopCoroutine(Reference.instance.currentKeypad.GetComponent<KeyPadInspector>().CheckPassword());
                }
                if (Reference.instance.dialogueCanvas.gameObject.activeInHierarchy)
                {
                    Reference.instance.dialogueCanvas.Close();
                }
                if (Reference.instance.diary.gameObject.activeInHierarchy)
                {
                    Reference.instance.diary.Close();
                }
                if (Reference.instance.jigsawCanvas.gameObject.activeInHierarchy)
                {
                    Reference.instance.jigsawCanvas.Close();
                }
            }
            else //If Jorunal open
            {
                Reference.instance.journalManager.CloseJournal();
            }
        }
        else if (Input.GetMouseButtonDown(1) && JournalOpen())
        {
            Reference.instance.journalManager.CloseJournal();
        }


        //if P is pressed: freeze game and show pause Menu
        if (Input.GetKeyDown(KeyCode.P)){
            if (gameManager.GameIsOnPause()){
                Reference.instance.pauseMenu.ResumeGame();  
            }
            else{
                Reference.instance.pauseMenu.PauseGame();
            }
        }

        //if F is pressed: switch flashlight on or off
        if (flashlightEnabled && Input.GetKeyDown(KeyCode.F))
        {
            if (Reference.instance.flashlight.gameObject.activeInHierarchy){
                Reference.instance.flashlight.gameObject.SetActive(false);
            }
            else{
                Reference.instance.flashlight.gameObject.SetActive(true);
            }
        }

        //if I is pressed: toggle journal
        if (Input.GetKeyDown(KeyCode.I))
        {
            Reference.instance.journalManager.OpenJournal();
        }

        if (!TutorialManager.tutorialManager.firstStep && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            TutorialManager.tutorialManager.firstStep = true;
            TutorialManager.tutorialManager.ShowJournalTut();

        }

        //if(CurrentlyInteracting() || GameIsOnPause())
        //{
        //    Reference.instance.crosshair.gameObject.SetActive(false);
        //}
        //else
        //{
        //    Reference.instance.crosshair.gameObject.SetActive(true);
        //}

    }

    #endregion

    #region keep track of journal
    public delegate void UpdateJournalInfo(JournalPage info);
    public static event UpdateJournalInfo OnNewJournalInfo;

    public void FireNewJournalEntry(JournalPage page)
    {
        if (OnNewJournalInfo != null)
        {
            OnNewJournalInfo(page);
        }
    }
    #endregion

    //not in use yet, maybe for lights?
    #region keep track of power
    public bool powerIsBack;

    public delegate void PowerIsBack();
    public static event PowerIsBack OnPowerIsBack;

    public void FirePowerIsBack()
    {
        if (OnPowerIsBack != null)
        {
            OnPowerIsBack();
        }
    }
    #endregion

    #region keep track of flashlight
    public bool flashlightEnabled;
    #endregion

    //is called by the "Canvas" Classes if the interactables in "Activate()" and "Close()"
    public void SwitchCameras(string mode)
    {
        switch (mode)
        {
            case "2D":
                //Camera.main.orthographic = true;

                Reference.instance.firstPersonCam.enabled = false;
                //Reference.instance.firstPersonCam.gameObject.SetActive(false);
                //Reference.instance.camera2D.gameObject.SetActive(true);
                Reference.instance.camera2D.enabled = true;
                break;

            case "3D":
                //Camera.main.orthographic = false;

                Reference.instance.camera2D.enabled = false;
                //Reference.instance.camera2D.gameObject.SetActive(false);
                Reference.instance.firstPersonCam.enabled = true;
                //Reference.instance.firstPersonCam.gameObject.SetActive(true);
                break;
        }
    }


    #region Test if stuff is happening
    //test if there is currently an Inspector up
    public bool CurrentlyInteracting()
    {
        return (InspectorOpen() || JournalOpen() || GameIsOnPause());
    }

    public bool InspectorOpen()
    {
        return (Reference.instance.ivCanvas.gameObject.activeInHierarchy ||
                Reference.instance.obsCam.gameObject.activeInHierarchy ||
                Reference.instance.keyPad.gameObject.activeInHierarchy ||
                Reference.instance.dialogueCanvas.gameObject.activeInHierarchy||
                Reference.instance.diary.gameObject.activeInHierarchy ||
                Reference.instance.jigsawCanvas.gameObject.activeInHierarchy);
    }

    public bool JournalOpen()
    {
        return Reference.instance.journal.gameObject.activeInHierarchy;
    }

    //test if game is on pause
    public bool GameIsOnPause()
    {
        return Reference.instance.pauseMenu.gameIsPaused;
    }
    #endregion


    //replaced by Image!
    //add a crosshair to the center of the screen
    //void OnGUI(){
    //    if (!gameManager.CurrentlyInteracting() && !gameManager.GameIsOnPause())
    //    {
    //        GUI.color = Color.green;
    //        GUI.Box(new Rect(Screen.width / 2, Screen.height / 2, 10, 10), "");
    //    }
    //}


}
