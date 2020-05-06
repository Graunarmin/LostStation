using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueReferences : MonoBehaviour
{

    #region singleton
    public static DialogueReferences diaRef;

    private void Awake()
    {
        if(diaRef == null)
        {
            diaRef = this;
        }
    }
    #endregion

    //reference all importad game objects here so they can be accessed from any script
    public GameObject testGameObject;
}
