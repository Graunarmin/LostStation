using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrosshairCanvas : MonoBehaviour
{
    public CursorIcon cursorIcon;

    public void ShowIcon(Interactable interactable)
	{
        if(interactable is DiaryInspector)
        {
            cursorIcon.read.gameObject.SetActive(true);
        }
	}

}
