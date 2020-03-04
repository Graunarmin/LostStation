using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageInspectorCanvas : MonoBehaviour
{

    public void Activate(Image pic)
    {
        //So we can't click on anything in the background
        GameManager.gameManager.SwitchCameras("2D");
        Time.timeScale = 0f;
        Reference.instance.currentItem.location.SetContainedItems(false);
        Reference.instance.currentItem.col.enabled = false;

        gameObject.SetActive(true);
        pic.gameObject.SetActive(true);

    }

    public void Close()
    {
        GameManager.gameManager.SwitchCameras("3D");
        Reference.instance.currentItem.location.SetContainedItems(true);
        Reference.instance.currentItem.col.enabled = true;

        Image []children = GetComponentsInChildren<Image>();

        foreach(Image child in children)
        {
            child.gameObject.SetActive(false);
        }

        gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
