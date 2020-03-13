using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotebookInspector : Interactable
{
    [SerializeField] OscarNotebookCanvas canvas;
    public override void Interact()
    {
        canvas.Activate();
    }
}
