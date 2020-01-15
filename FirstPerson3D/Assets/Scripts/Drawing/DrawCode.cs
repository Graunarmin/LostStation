using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DrawCode : MonoBehaviour//, IPointerClickHandler
{
    public static DrawCode drawCode;

    [SerializeField]
    private GameObject lineHolderPrefab;
    public List<LinePoint> form;
    public GameObject lockscreen;
    public GameObject textArea;
    public bool firstClickOccured;
    public bool secondClickOccured;
    private LinePoint startPoint;
    private LinePoint nextPoint;
    private List<LinePoint> clickedPoints = new List<LinePoint>();


    private void Awake()
    {
        if (drawCode == null)
        {
            drawCode = this;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Enter();
        }
    }

    public void Enter()
    {
        bool correctForm = TestForm();
        Debug.Log(correctForm);

        DeleteForm();
        if (correctForm)
        {
            lockscreen.gameObject.SetActive(false);
            textArea.gameObject.SetActive(true);
        }
    }

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

    private bool TestForm()
    {
        if (clickedPoints.Count != form.Count)
            return false;

        return compareLists(clickedPoints, form);
    }

    private bool compareLists(List<LinePoint> list1, List<LinePoint> list2)
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