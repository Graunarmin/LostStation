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
        

    public bool Close()
    {
        StopCoroutine(Reference.instance.currentItem.GetComponent<JigsawInspector>().CheckSolution());
        JigsawManager.jigsawManager.ResetGame();
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        GameManager.gameManager.SwitchCameras("3D");
        return true;
    }
}
