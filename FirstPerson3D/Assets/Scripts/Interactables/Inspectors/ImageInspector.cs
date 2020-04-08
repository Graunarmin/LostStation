using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageInspector : Interactable
{

    public Image pic;
    public ImageInspectorCanvas inspectorCanvas;

    public override void Interact()
    {
        Reference.instance.ivCanvas.Activate(pic);
        //inspectorCanvas.Activate(pic);
        //Test if this is the first ever Canvas that opens and if so, show Tutorial on how to clos
        TutorialManager.tutorialManager.FirstCanvas();
    }
}
