using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiaryCanvas : MonoBehaviour
{
    private JournalPage journalPage;

    [SerializeField] Lockscreen lockscreen;

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
        //unfreeze game
        Time.timeScale = 1f;
        //hide canvas
        gameObject.SetActive(false);
        GameManager.gameManager.SwitchCameras("3D");

        if (lockscreen.unlocked)
        {
            //add journalPage to journal
            JournalManager.journalManager.UpdateJournal(journalPage);
            journalPage = null;
        }
        DrawManager.pattern.DeleteForm();

        //if (gameObject.GetComponent<DrawManager>() != null)
        //{
        //    if (gameObject.GetComponent<DrawManager>().lockscreen.unlocked)
        //    {
        //        //add journalPage to journal
        //        JournalManager.journalManager.UpdateJournal(journalPage);
        //        journalPage = null;
        //    }
        //}
        
    }
}
