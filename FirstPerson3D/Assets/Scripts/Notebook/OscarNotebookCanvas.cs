using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OscarNotebookCanvas : Notebook, IPuzzleCanvas
{
    public void Activate()
    {
        GameManager.gameManager.SwitchCameras("2D");
        gameObject.SetActive(true);
        //freeze game
        Time.timeScale = 0f;
        //show first page
        currentPage = allPages[0];
        background.GetComponent<Image>().sprite = currentPage.pagePic;
        OpenNotebook();
    }

    public void Close()
    {
        HideText();
        gameObject.SetActive(false);
        CloseNotebook();
        Time.timeScale = 1f;
    }
}
