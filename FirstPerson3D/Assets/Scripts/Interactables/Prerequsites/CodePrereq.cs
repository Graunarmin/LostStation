using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CodePrereq : Prerequisite
{

    //check if Prerequisite is met
    public override bool Complete
    {
        get
        {
            return GetComponent<Door>().DoorIsUnlocked();
            //if (Reference.instance.currentDoor != null)
            //{
            //    return Reference.instance.currentDoor.connectedKeypad.passwordCorrect;
            //}
            //else
            //{
            //    return false;
            //}
        }
    }
}
