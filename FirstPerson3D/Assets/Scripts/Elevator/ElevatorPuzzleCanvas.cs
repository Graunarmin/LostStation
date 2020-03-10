using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPuzzleCanvas : MonoBehaviour, IPuzzleCanvas
{
    [SerializeField] ElevatorControlsCanvas controls;

    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        //only return to Controls Canvas, don't close everything completely
        gameObject.SetActive(false);
        controls.Reactivate();
    }
}
