using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageInspectorCanvas : MonoBehaviour
{
    public Image imageHolder;

    public void Activate(Sprite pic)
    {
        //So we can't click on anything in the background
        GameManager.gameManager.SwitchCameras("2D");
        Time.timeScale = 0f;
        Reference.instance.currentItem.location.SetContainedItems(false);
        Reference.instance.currentItem.col.enabled = false;

        gameObject.SetActive(true);
        imageHolder.sprite = pic;

    }

    public void Close()
    {
        GameManager.gameManager.SwitchCameras("3D");
        Reference.instance.currentItem.location.SetContainedItems(true);
        Reference.instance.currentItem.col.enabled = true;

        gameObject.SetActive(false);
        imageHolder.sprite = null;
        Time.timeScale = 1f;
    }
}
