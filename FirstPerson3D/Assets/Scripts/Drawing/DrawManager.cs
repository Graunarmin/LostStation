using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//adapted, used in game
public class DrawManager : MonoBehaviour//, IPointerClickHandler
{

    #region Singleton
    public static DrawManager pattern;

    private void Awake()
    {
        if (pattern == null)
        {
            pattern = this;
        }
    }
    #endregion

    [SerializeField] GameObject lineHolderPrefab;
    public bool firstClickOccured;
    public bool secondClickOccured;
    private LinePoint startPoint;
    private LinePoint nextPoint;
    private List<LinePoint> clickedPoints = new List<LinePoint>();

    #region drawing
    public void HandlePointClick(LinePoint point)
    {
        if (!firstClickOccured)
        {
            SetStart(point);
        }
        else
        {
            SetNextPoint(point);
        }
    }

    private void SetStart(LinePoint point)
    {
        startPoint = point;
        Debug.Log("Add " + point.name + " to List");
        clickedPoints.Add(point);
        firstClickOccured = true;
    }

    private void SetNextPoint(LinePoint point)
    {
        //If we are waiting for the second klick we can't change the startpoint
        if (!secondClickOccured)
        {
            secondClickOccured = true;
        }
        else
        {
            startPoint = nextPoint;
        }
        //in any case the next point is the one we just clicked and
        //we want to draw a line from the start to here
        nextPoint = point;
        Debug.Log("Add " + point.name + " to List");
        clickedPoints.Add(point);
        DrawLine(startPoint, nextPoint);

    }

    private void DrawLine(LinePoint pos1, LinePoint pos2)
    {
        GameObject newLineGen = Instantiate(lineHolderPrefab);
        LineRenderer lRend = newLineGen.GetComponent<LineRenderer>();

        lRend.positionCount = 2;
        lRend.SetPosition(0, new Vector3(pos1.pointPos.x, pos1.pointPos.y, 20));
        lRend.SetPosition(1, new Vector3(pos2.pointPos.x, pos2.pointPos.y, 20));
    }
    #endregion

    #region testing
    public bool TestForm(List<LinePoint> form)
    {
        if (clickedPoints.Count != form.Count)
        {
            return false;
        }
        return CompareLists(clickedPoints, form);
    }

    private bool CompareLists(List<LinePoint> list1, List<LinePoint> list2)
    {
        for (int n = 0; n < list1.Count; n++)
        {
            if (list1[n].name != list2[n].name)
            {
                return false;
            }
        }
        return true;
    }
    #endregion

    //reset everything
    public void DeleteForm()
    {
        GameObject[] allLines = GameObject.FindGameObjectsWithTag("DrawingLine");

        foreach (GameObject line in allLines)
        {
            Destroy(line);
        }

        //Set all clicked Points to not clicked
        clickedPoints.Clear();
        firstClickOccured = false;
        secondClickOccured = false;
        startPoint = null;
        nextPoint = null;
    }
}