using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryCanvas : MonoBehaviour
{
    private JournalPage journalPage;

    public void Activate(JournalPage jPage)
    {
        //Camera.main.orthographic = true;
        GameManager.gameManager.SwitchCameras("2D");
        //show canvas
        gameObject.SetActive(true);
        //freeze game
        Time.timeScale = 0f;
        journalPage = jPage;
    }

    public void Close()
    {
        //Camera.main.orthographic = false;
        GameManager.gameManager.SwitchCameras("3D");
        //hide canvas
        gameObject.SetActive(false);
        //unfreeze game
        Time.timeScale = 1f;

        //add journalPage to journal
       JournalManager.journalManager.UpdateJournal(journalPage);
        journalPage = null;
    }
}
