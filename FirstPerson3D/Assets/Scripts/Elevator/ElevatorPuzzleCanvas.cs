using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPuzzleCanvas : MonoBehaviour, IPuzzleCanvas
{
    [SerializeField] ElevatorControlsCanvas controls;
    [SerializeField] List<LinePoint> form;
    public bool circuitSet;


    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public void Close()
    {
        //only return to Controls Canvas, don't close everything completely
        gameObject.SetActive(false);
        controls.ReactivateAfterCircuit();
    }

    private void SetCircuit()
    {
        circuitSet = true;
        Close();
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
        //Debug.Log(correctForm);

        DrawManager.pattern.DeleteForm();
        if (correctForm)
        {
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.passwordCorrect);
            SetCircuit();
        }
    }

    private bool TestForm()
    {
        return DrawManager.pattern.TestForm(form);
    }
}
