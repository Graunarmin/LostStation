using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInspector : Interactable
{

    public override void Interact()
    {
        //Test if this is the first ever Canvas that opens and if so, show Tutorial on how to clos
        TutorialManager.tutorialManager.FirstCanvas();

        //"duplicate" the clicked on Object
        GameObject item = Instantiate(gameObject);

        //set the rig as the parent (as in the unity history)
        item.transform.SetParent(Reference.instance.obsCam.rig);

        //bring it to the middle of the observer Camera's Screen
        item.transform.localPosition = Vector3.zero;

        //push the model of the Prop down so the pivot is aligned with the "actual" Prop
        item.transform.GetChild(0).localPosition = Vector3.zero;

        Reference.instance.obsCam.model = item.transform;

        //turn on observer Camera
        Reference.instance.obsCam.Activate();

        //Test if this is the first ever Canvas that opens and if so, show Tutorial on how to clos
        TutorialManager.tutorialManager.FirstCanvas();

    }
}
