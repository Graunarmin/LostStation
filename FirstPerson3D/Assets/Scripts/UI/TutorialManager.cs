using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    //public TextMeshProUGUI pauseGame;
    public TextMeshProUGUI openJournal;
    public TextMeshProUGUI closeOverlay;
   

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

    private IEnumerator CloseTutorial()
    {
        yield return new WaitForSeconds(2);
        Reference.instance.camera2D.enabled = false;
        Reference.instance.gameUICanvas.gameObject.SetActive(false);
        openJournal.gameObject.SetActive(false);
        closeOverlay.gameObject.SetActive(false);
    }
}
