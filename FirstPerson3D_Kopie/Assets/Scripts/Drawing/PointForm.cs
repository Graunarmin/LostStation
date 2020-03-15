using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PointForm", menuName = "Point Form")]

public class PointForm : ScriptableObject
{
    public string formName;
    public List<LinePoint> pointOrder;
}