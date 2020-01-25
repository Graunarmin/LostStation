using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockscreen : MonoBehaviour
{

    public bool unlocked;

    public void Unlock()
    {
        unlocked = true;
        gameObject.SetActive(false);
    }
}
