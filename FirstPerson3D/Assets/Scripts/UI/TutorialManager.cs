using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject openJournal;
    public GameObject openInventory;
    public GameObject closeOverlay;
    public GameObject toggleFlashlight;

    [HideInInspector]
    public bool firstStep;
    [HideInInspector]
    public bool firstCanvas;
    [HideInInspector]
    public bool firstCollectable;

    private List<GameObject> allTutorials = new List<GameObject>();

    #region singleton
    public static TutorialManager tutorialManager;

    private void Awake()
    {
        if (tutorialManager == null)
        {
            tutorialManager = this;
        }
        openJournal.gameObject.SetActive(false);
        openInventory.gameObject.SetActive(false);
        closeOverlay.gameObject.SetActive(false);
        toggleFlashlight.gameObject.SetActive(false);

    }
    #endregion

    public void FirstCanvas()
    {
        if (!firstCanvas)
        {
            firstCanvas = true;
            ShowCloseTut();
        }
    }

    public void FirstCollectable()
    {
        if (!firstCollectable)
        {
            firstCollectable = true;
            StartCoroutine(WaitForEndOfInspect());
        }
    }

    private IEnumerator WaitForEndOfInspect()
    {
        yield return new WaitUntil(()
            => !Reference.instance.obsCam.gameObject.activeInHierarchy);

        yield return new WaitForSecondsRealtime(0.5f);

        ShowInventoryTut();
    }

    public void ShowInventoryTut()
    {
        Reference.instance.camera2D.enabled = true;
        Reference.instance.gameUICanvas.gameObject.SetActive(true);
        openInventory.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial(openInventory));
    }

    public void ShowJournalTut()
    {
        Reference.instance.camera2D.enabled = true;
        Reference.instance.gameUICanvas.gameObject.SetActive(true);
        openJournal.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial(openJournal));
    }

    public void ShowCloseTut()
    {
        Reference.instance.camera2D.enabled = true;
        Reference.instance.gameUICanvas.gameObject.SetActive(true);
        closeOverlay.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial(closeOverlay));
    }

    //Is called from DialogueEvents when the flashlight is enabled
    public IEnumerator WaitForEndOfDialogue()
    {
        yield return new WaitUntil(()
            => !Reference.instance.dialogueCanvas.gameObject.activeInHierarchy);

        yield return new WaitForSecondsRealtime(0.5f);

        ShowFlashlightTut();
    }

    private void ShowFlashlightTut()
    {
        Reference.instance.camera2D.enabled = true;
        Reference.instance.gameUICanvas.gameObject.SetActive(true);
        toggleFlashlight.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial(toggleFlashlight));
    }


    private IEnumerator CloseTutorial(GameObject tutorial)
    {
        yield return new WaitForSecondsRealtime(2f);

        if (!GameManager.gameManager.CurrentlyInteracting() && !OtherTutorialOpen())
        {
            Reference.instance.camera2D.enabled = false;
            Reference.instance.gameUICanvas.gameObject.SetActive(false);
        }

        tutorial.SetActive(false);
    }

    private bool OtherTutorialOpen()
    {
        //match eatch tutorial with all others
        foreach(GameObject tutorial in allTutorials)
        {
            foreach(GameObject tut2 in allTutorials)
            {
                //if they are not the same
                if(tutorial != tut2)
                {
                    //and if they are both active
                    if (tutorial.activeInHierarchy && tut2.activeInHierarchy)
                    {
                        return true;
                    }
                }
                
            }
        }
        return false;
    }
}
