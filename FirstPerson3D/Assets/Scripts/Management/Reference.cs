using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Reference : MonoBehaviour
{
    #region singleton
    public static Reference instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        //deactivate all the stuff that is not visible yet
        crosshair.gameObject.SetActive(true);
        flashlight.gameObject.SetActive(false);
        pauseMenu.pauseMenuUI.gameObject.SetActive(false);
        dialogueCanvas.gameObject.SetActive(false);
        objectInfoDisplay.gameObject.SetActive(false);
        gameUICanvas.gameObject.SetActive(false);
        journal.gameObject.SetActive(false);
        journalUpdateInfo.gameObject.SetActive(false);

        ivCanvas.gameObject.SetActive(false);
        obsCam.gameObject.SetActive(false);
        keyPad.gameObject.SetActive(false);
        diary.gameObject.SetActive(false);
        jigsawCanvas.gameObject.SetActive(false);

    }
    #endregion


    #region references
    public CrosshairCanvas crosshair;

    public Camera camera2D;
    public Camera firstPersonCam;

    public Transform player;
    public Light flashlight;

    public GamePauseMenu pauseMenu;

    public DialogueManager dialogueManager;
    public DialogueCanvas dialogueCanvas;

    public Canvas gameUICanvas;
    public InventoryDispaly inventoryDisplay;
    public Canvas objectInfoDisplay;
    public TextMeshProUGUI ObjectInfoDisplayText;

    public JournalManager journalManager;
    public Canvas journal;
    public Canvas journalUpdateInfo;

    public ImageInspectorCanvas ivCanvas;
    public ObjectInspectorCam obsCam;
    public KeyPadCanvas keyPad;
    public DiaryCanvas diary;
    public JigsawCanvas jigsawCanvas;


    //to be fille by code
    [HideInInspector]
    public List<Region> currentRegions = new List<Region>();
    [HideInInspector]
    public Item currentItem;
    [HideInInspector]
    public Keypad currentKeypad;
    [HideInInspector]
    public Door currentDoor;

    public Collectable collectHeld = null;

    public bool currentlyInteracting;

    #endregion
}

