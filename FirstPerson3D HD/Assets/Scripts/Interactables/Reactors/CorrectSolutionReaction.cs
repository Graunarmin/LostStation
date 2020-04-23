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
            if (AllPrerequsComplete())
            {
                reaction.gameObject.SetActive(true);
            }
        }
    }

    public void UndoReaction()
    {
        if (reaction != null)
        {
            reaction.gameObject.SetActive(false);
        }
    }

    public bool AllPrerequsComplete()
    {
        var prerequisites = reaction.gameObject.GetComponents<Prerequisite>();

        //either there is none - in which case it's "complete"
        if (prerequisites.Length == 0)
        {
            //Debug.Log("No Prerequisites");
            return true;
        }

        //or we have to test each prerequ and as soon as one is not met, it's incomplete
        foreach (Prerequisite p in prerequisites)
        {
            p.Print();
            if (!p.Complete)
            {
                return false;
            }
        }
        return true;
    }
}
