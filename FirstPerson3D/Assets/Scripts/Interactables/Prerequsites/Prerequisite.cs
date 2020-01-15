using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prerequisite : MonoBehaviour
{

    //can't access the item until a prerequisite is met
    public bool itemAccess;

    //check if Prerequisite is met
    public virtual bool Complete{
        get {

            return false;
        }
    }
}
