using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageInspector : Interactable
{

    public Sprite pic;

    public override void Interact()
    {
        Reference.instance.ivCanvas.Activate(pic);
    }
}
