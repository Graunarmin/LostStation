using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LinePoint : MonoBehaviour, IPointerClickHandler
{
    public bool pointClicked;
    public Vector3 pointPos;

    //Draw lines between the actually clicked points (on screen)
    //save order of "stars" clicked and compare to predefined form forewards and backwards
    //if form correct: leave lines where they are and make "stars" not clickable anymore
    //if form incorrect: delete all lines (destroy)


    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //if point was not already clicked:
        //if (!pointClicked)
        //{
            pointPos = Reference.instance.camera2D.ScreenToWorldPoint(pointerEventData.position);
            //pointClicked = true;

            //Debug.Log(name + " Game Object Clicked at " + pointerEventData.position);
            //Debug.Log("My position: " + pointPos);

            DrawManager.pattern.HandlePointClick(this);
        //}

    }
}
