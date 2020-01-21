using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    //public TextMeshProUGUI pauseGame;
    public TextMeshProUGUI openJournal;
    public TextMeshProUGUI openInventory;
    public TextMeshProUGUI closeOverlay;
    public TextMeshProUGUI toggleFlashlight;

    [HideInInspector]
    public bool firstStep;
    [HideInInspector]
    public bool firstCanvas;
    [HideInInspector]
    public bool firstCollectable;

    #region singleton
    public static TutorialManager tutorialManager;

    private void Awake()
    {
        if (tutorialManager == null)
        {
            tutorialManager = this;
        }
        openJournal.gameObject.SetActive(false);
        closeOverlay.gameObject.SetActive(false);

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

    public void ShowInventoryTut()
    {
        Reference.instance.camera2D.enabled = true;
        Reference.instance.gameUICanvas.gameObject.SetActive(true);
        openInventory.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial());
    }

    public void ShowJournalTut()
    {
        Reference.instance.camera2D.enabled = true;
        Reference.instance.gameUICanvas.gameObject.SetActive(true);
        openJournal.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial());
    }

    public void ShowCloseTut()
    {
        Reference.instance.camera2D.enabled = true;
        Reference.instance.gameUICanvas.gameObject.SetActive(true);
        closeOverlay.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial());
    }

    private void ShowFlashlightTut()
    {
        Reference.instance.camera2D.enabled = true;
        Reference.instance.gameUICanvas.gameObject.SetActive(true);
        toggleFlashlight.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial());
    }

    private IEnumerator CloseTutorial()
    {
        yield return new WaitForSecondsRealtime(2f);

        if (!GameManager.gameManager.CurrentlyInteracting())
        {
            Reference.instance.camera2D.enabled = false;
        }

        Reference.instance.gameUICanvas.gameObject.SetActive(false);
        openJournal.gameObject.SetActive(false);
        closeOverlay.gameObject.SetActive(false);
        toggleFlashlight.gameObject.SetActive(false);
    }

    public IEnumerator WaitForEndOfDialogue()
    {
        yield return new WaitUntil(()
            => !Reference.instance.dialogueCanvas.gameObject.activeInHierarchy);

        yield return new WaitForSecondsRealtime(0.5f);

        ShowFlashlightTut();
    }

    public IEnumerator WaitForEndOfInspect()
    {
        yield return new WaitUntil(()
            => !Reference.instance.obsCam.gameObject.activeInHierarchy);

        yield return new WaitForSecondsRealtime(0.5f);

        ShowInventoryTut();
    }
}
