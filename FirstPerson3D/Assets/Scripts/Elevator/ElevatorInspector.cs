using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorInspector : Interactable
{

    [SerializeField] ElevatorControlsCanvas controls;

    public override void Interact()
    {
        controls.Activate();
    }
}
