using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiaryInspector : Interactable
{
    public DiaryCanvas diary;
    public JournalPage journalPage;

    public override void Interact()
    {
        diary.Activate(journalPage);

        //Test if this is the first ever Canvas that opens and if so, show Tutorial on how to clos
        TutorialManager.tutorialManager.FirstCanvas();
    }
}

