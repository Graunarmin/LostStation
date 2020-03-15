using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultManager : ItemContainerManager
{
    #region Singleton
    public static ResultManager resManager;
    private void Awake()
    {

        if (resManager == null)
        {
            resManager = this;
        }
        else
        {
            Debug.LogWarning("More than one instance of ResultManager!");
        }

    }
    #endregion
}
