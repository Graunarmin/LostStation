using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchPrereq : Prerequisite
{
    //every switcher starts off as false!
    // e.g: door locked means false, door unlocked means true
    public Switcher observedSwitcher;

    //check if Prerequisite is met
    public override bool Complete{
        get{
            //observe switch and return true or false (active/inactive)
            return observedSwitcher.state;
        }
    }

    public override void Print()
    {
        Debug.Log("Switcher Prerequisite");
    }
}

