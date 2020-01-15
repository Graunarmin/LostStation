using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New JournalPage", menuName = "JournalPage")]
public class PageInfo : ScriptableObject
{
    public bool doublePage;
    public int pageNumber;
    public Sprite pagePic;
}

