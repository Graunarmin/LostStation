using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//So item can be safed and appears in inspector
//[System.Serializable]
public class Collectable : Item
{
    public string collectName;
    [TextArea(2,5)]
    public string useMessage;

    public override void ManageInteractables()
    {
        var hasPrerequ = GetComponent<Prerequisite>();
        //make item interactable, if prerequisite is met
        if (interactable != null)
        {
            if (AllPrerequsComplete())
            {
                interactable.enabled = true;
                //CheckForCollectable();
                interactable.Interact();
            }
        }
    }
}
