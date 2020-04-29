using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject move;
    public TextMeshProUGUI moveText;
    public GameObject sprint;
    public TextMeshProUGUI sprintText;
    public GameObject openJournal;
    public GameObject openInventory;
    public GameObject closeOverlay;
    public GameObject toggleFlashlight;
    public GameObject object3D;

    [HideInInspector]
    public bool firstStep;
    [HideInInspector]
    public bool firstCanvas;
    [HideInInspector]
    public bool firstCollectable;
    [HideInInspector]
    public bool first3DObject;

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
        StartCoroutine(ShowOpeningTutorial());

    }
    #endregion

    private IEnumerator ShowOpeningTutorial()
    {
        yield return new WaitForSeconds(10f);
        ShowMoveTut();
    }

    #region Test if first
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

    public void First3DObject()
    {
        if (!first3DObject)
        {
            first3DObject = true;
            ShowRotateTut();
        }
    }
    #endregion

    #region MoveTutorials
    public void ShowMoveTut()
    {
        Reference.instance.TutorialCanvas.gameObject.SetActive(true);
        move.SetActive(true);
        StartCoroutine(FadeTextToFullAlpha(1f, moveText));
        StartCoroutine(CloseOpeningTut());
    }

    private IEnumerator CloseOpeningTut()
    {
        yield return new WaitForSeconds(4f);
        
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(FadeTextToFullAlpha(1f, sprintText));

        sprint.gameObject.SetActive(true);
        yield return new WaitForSeconds(4f);
        move.SetActive(false);
        sprint.SetActive(false);
        Reference.instance.TutorialCanvas.gameObject.SetActive(false);

    }

    public IEnumerator FadeTextToFullAlpha(float t, TextMeshProUGUI textElement)
    {
        //first set alpha to zero
        textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, 0.0f);

        //the fade in text
        while (textElement.color.a < 1.0f)
        {
            textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, textElement.color.a + (Time.deltaTime / t));
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeTextToZeroAlpha(1f, textElement));
    }

    public IEnumerator FadeTextToZeroAlpha(float t, TextMeshProUGUI textElement)
    {
        textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, 1.0f);
        //the fade in text
        while (textElement.color.a > 0.0f)
        {
            textElement.color = new Color(textElement.color.r, textElement.color.g, textElement.color.b, textElement.color.a - (Time.deltaTime / t));
            yield return null;
        }
    }
    #endregion

    #region Show Tutorials
    public void ShowInventoryTut()
    {
        Reference.instance.TutorialCanvas.gameObject.SetActive(true);
        openInventory.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial(openInventory, 3f));
    }

    public void ShowJournalTut()
    {
        Reference.instance.TutorialCanvas.gameObject.SetActive(true);
        openJournal.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial(openJournal, 2f));
    }

    public void ShowCloseTut()
    {
        Reference.instance.TutorialCanvas.gameObject.SetActive(true);
        closeOverlay.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial(closeOverlay, 2f));
    }

    public void ShowRotateTut()
    {
        Reference.instance.TutorialCanvas.gameObject.SetActive(true);
        object3D.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial(object3D, 2f));
    }

    private void ShowFlashlightTut()
    {
        Reference.instance.TutorialCanvas.gameObject.SetActive(true);
        toggleFlashlight.gameObject.SetActive(true);
        StartCoroutine(CloseTutorial(toggleFlashlight, 2f));
    }
    #endregion

    #region Wait for Stuff
    //Is called from DialogueEvents when the flashlight is enabled
    public IEnumerator WaitForEndOfDialogue()
    {
        yield return new WaitUntil(()
            => !Reference.instance.dialogueCanvas.gameObject.activeInHierarchy);

        yield return new WaitForSecondsRealtime(0.5f);

        ShowFlashlightTut();
    }

    private IEnumerator WaitForEndOfInspect()
    {
        yield return new WaitUntil(()
            => !Reference.instance.obsCam.gameObject.activeInHierarchy);

        yield return new WaitForSecondsRealtime(0.5f);

        ShowInventoryTut();
    }

    private bool OtherTutorialOpen()
    {
        //match each tutorial with all the others
        foreach (GameObject tutorial in allTutorials)
        {
            foreach (GameObject tut2 in allTutorials)
            {
                //if they are not the same
                if (tutorial != tut2)
                {
                    //and if they are both active
                    if (tutorial.activeInHierarchy && tut2.activeInHierarchy)
                    {
                        //then two tutorials are open and the camera can not yet be switched
                        return true;
                    }
                }

            }
        }
        return false;
    }

    #endregion


    private IEnumerator CloseTutorial(GameObject tutorial, float time)
    {
        yield return new WaitForSecondsRealtime(time);

        if (!GameManager.gameManager.CurrentlyInteracting() && !OtherTutorialOpen())
        {
            Reference.instance.TutorialCanvas.gameObject.SetActive(false);
        }

        tutorial.SetActive(false);
    }

    
}
