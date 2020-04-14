using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPuzzleCanvas : MonoBehaviour, IPuzzleCanvas
{
    [SerializeField] ElevatorControlsCanvas controls;
    [SerializeField] List<LinePoint> form;
    public bool circuitSet;

    public CorrectSolutionReaction reaction;


    public void Activate()
    {
        gameObject.SetActive(true);
    }

    public bool Close()
    {
        //only return to Controls Canvas, don't close everything completely
        gameObject.SetActive(false);
        controls.ReactivateAfterCircuit();
        return true;
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
            //React in some way
            if (reaction != null)
            {
                reaction.React();
            }
            AudioManager.audioManager.PlaySound(AudioManager.audioManager.solutionCorrect);
            SetCircuit();
        }
    }

    public void CloseCanvas()
    {
        Close();
    }

    private bool TestForm()
    {
        return DrawManager.pattern.TestForm(form);
    }
}
