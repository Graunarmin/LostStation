using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    #region Singleton
    public static GameManager gameManager;

    //very bad singleton, look up how it's done properly!!
    private void Awake()
    {

        if (gameManager == null)
        {
            gameManager = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of GameManager!");
        }

    }
    #endregion

    #region userinput

    private KeyCode flashlight = KeyCode.F;
    private KeyCode inventory = KeyCode.I;
    private KeyCode journal = KeyCode.J;
    private KeyCode pause = KeyCode.P;

    public void SetInventoryKey(KeyCode code)
    {
        inventory = code;
    }

    private void Update(){
        //on rightclick: close imageviewer/ oberserver cam / Keypad / Puzzle / DrawingPanel
        if (Input.GetMouseButtonDown(1))// && Reference.instance.currentItem != null)
        {
            if (!JournalOpen() && !InventoryOpen())
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
                if (Reference.instance.oscarsNotebook.gameObject.activeInHierarchy)
                {
                    Reference.instance.oscarsNotebook.Close();
                }
            }
            //else if both are open close both
            else if (InventoryOpen() && JournalOpen())
            {
                InventoryManager.invManager.CloseInventory();
                JournalManager.journalManager.CloseNotebook();
            }
            //if inventory open
            else if (InventoryOpen())
            {
                InventoryManager.invManager.CloseInventory();
            }
            //If Jorunal open
            else if (JournalOpen())
            {
                JournalManager.journalManager.CloseNotebook();
            }
        }
        else if (Input.GetMouseButtonDown(1) && JournalOpen() && !InventoryOpen())
        {
            JournalManager.journalManager.CloseNotebook();
        }
        else if (Input.GetMouseButtonDown(1) && InventoryOpen() && !JournalOpen())
        {
            InventoryManager.invManager.CloseInventory();
        }
        else if (Input.GetMouseButtonDown(1) && InventoryOpen() && JournalOpen())
        {
            InventoryManager.invManager.CloseInventory();
            JournalManager.journalManager.CloseNotebook();
        }


        //if P is pressed: freeze game and show pause Menu
        if (Input.GetKeyDown(pause)){
            if (gameManager.GameIsOnPause()){
                Reference.instance.pauseMenu.ResumeGame();  
            }
            else{
                Reference.instance.pauseMenu.PauseGame();
            }
        }

        //if F is pressed: switch flashlight on or off
        if (flashlightEnabled && Input.GetKeyDown(flashlight))
        {
            if (Reference.instance.flashlight.gameObject.activeInHierarchy){
                Reference.instance.flashlight.gameObject.SetActive(false);
            }
            else{
                Reference.instance.flashlight.gameObject.SetActive(true);
            }
        }

        //if J is pressed: toggle journal
        if (Input.GetKeyDown(journal))
        {
            JournalManager.journalManager.OpenNotebook();
        }

        //if I is pressed: toggle inventory
        if (Input.GetKeyDown(inventory))
        {
           InventoryManager.invManager.OpenInventory();
        }

        //Show first Tutorial on first move
        if (!TutorialManager.tutorialManager.firstStep && (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) ||
            Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)))
        {
            TutorialManager.tutorialManager.firstStep = true;
            TutorialManager.tutorialManager.ShowJournalTut();

        }
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

    #region Camera
    //is called by the "Canvas" Classes if the interactables in "Activate()" and "Close()"
    public void SwitchCameras(string mode)
    {
        switch (mode)
        {
            case "2D":
                //Camera.main.orthographic = true;

                Reference.instance.firstPersonCam.enabled = false;
                Reference.instance.firstPersonCam.gameObject.SetActive(false);

                Reference.instance.backgroundCam.gameObject.SetActive(true);
                Reference.instance.backgroundCam.enabled = true;
                Reference.instance.camera2D.enabled = true;
                break;

            case "3D":
                //Camera.main.orthographic = false;

                Reference.instance.camera2D.enabled = false;
                Reference.instance.backgroundCam.enabled = false;
                Reference.instance.backgroundCam.gameObject.SetActive(false);

                Reference.instance.firstPersonCam.gameObject.SetActive(true);
                Reference.instance.firstPersonCam.enabled = true;
                
                break;
        }
    }

    public void SwitchOn2DCam()
    {
        Reference.instance.camera2D.enabled = true;
    }

    public void SwitchOff2DCam()
    {
        if (!CurrentlyInteracting())
        {
            Reference.instance.camera2D.enabled = false;
        }
    }
    #endregion

    #region Test if stuff is happening
    //test if there is currently an Inspector up
    public bool CurrentlyInteracting()
    {
        return (InspectorOpen() || JournalOpen() || GameIsOnPause() || InventoryOpen());
    }

    public bool InspectorOpen()
    {
        return (Reference.instance.ivCanvas.gameObject.activeInHierarchy ||
                Reference.instance.obsCam.gameObject.activeInHierarchy ||
                Reference.instance.keyPad.gameObject.activeInHierarchy ||
                Reference.instance.dialogueCanvas.gameObject.activeInHierarchy||
                Reference.instance.diary.gameObject.activeInHierarchy ||
                Reference.instance.jigsawCanvas.gameObject.activeInHierarchy ||
                Reference.instance.oscarsNotebook.gameObject.activeInHierarchy);
    }

    public bool Crafting()
    {
        return Reference.instance.craftingArea.gameObject.activeInHierarchy;
    }

    public bool JournalOpen()
    {
        return Reference.instance.journal.gameObject.activeInHierarchy;
    }

    public bool InventoryOpen()
    {
        return Reference.instance.inventory.gameObject.activeInHierarchy;
    }

    //test if game is on pause
    public bool GameIsOnPause()
    {
        return Reference.instance.pauseMenu.gameIsPaused;
    }
    #endregion
}
