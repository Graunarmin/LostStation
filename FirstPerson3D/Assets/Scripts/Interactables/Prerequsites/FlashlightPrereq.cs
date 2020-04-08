using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightPrereq : Prerequisite
{
    public override bool Complete
    {
        get
        {
            if (Reference.instance.flashlight.IsInPossession())
            {
                return true;
            }
            return false;
        }
    }
}

