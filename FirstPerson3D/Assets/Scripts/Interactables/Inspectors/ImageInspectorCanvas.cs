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
        Reference.instance.currentItem.location.SetContainedItems(false);
        Reference.instance.currentItem.col.enabled = false;

        gameObject.SetActive(true);
        imageHolder.sprite = pic;

    }

    public void Close()
    {
        Reference.instance.currentItem.location.SetContainedItems(true);
        Reference.instance.currentItem.col.enabled = true;

        gameObject.SetActive(false);
        imageHolder.sprite = null;
    }
}
