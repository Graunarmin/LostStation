using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageInspector : Interactable
{

    public Sprite pic;

    public override void Interact()
    {
        Reference.instance.ivCanvas.Activate(pic);
        //Test if this is the first ever Canvas that opens and if so, show Tutorial on how to clos
        TutorialManager.tutorialManager.FirstCanvas();
    }
}
