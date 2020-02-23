using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JigsawCanvas : MonoBehaviour, IPuzzleCanvas
{

    public bool solved;

    public void Activate()
    {
        if (!solved)
        {
            GameManager.gameManager.SwitchCameras("2D");
            gameObject.SetActive(true);
            Time.timeScale = 0f;
        }
    }
        

    public void Close()
    {
        GameManager.gameManager.SwitchCameras("3D");
        StopCoroutine(Reference.instance.currentItem.GetComponent<JigsawInspector>().CheckSolution());
        JigsawManager.jigsawManager.ResetGame();
        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
