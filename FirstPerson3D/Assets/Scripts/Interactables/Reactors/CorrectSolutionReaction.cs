using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CorrectSolutionReaction
{
    //everything that's supposed to happen can happen in here.

    //All elements here:
    [SerializeField] GameObject reaction;

    public void React()
    {
        //And all actions in here
        if(reaction != null)
        {
            reaction.gameObject.SetActive(true);
        }
        
    }

    public void UndoReaction()
    {
        if (reaction != null)
        {
            reaction.gameObject.SetActive(false);
        }
    }
}
