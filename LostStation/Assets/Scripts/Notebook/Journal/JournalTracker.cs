using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalTracker : MonoBehaviour
{

    #region singleton
    public static JournalTracker journalTracker;

    private void Awake()
    {
        if (journalTracker == null)
        {
            journalTracker = this;
        }

        foreach(JournalPage jp in pages)
        {
            journalPages.Add(jp, jp.visible);
        }
    }
    #endregion


    public List<JournalPage> pages = new List<JournalPage>();

    [HideInInspector]
    public Dictionary<JournalPage, bool> journalPages = new Dictionary<JournalPage, bool>();

}
