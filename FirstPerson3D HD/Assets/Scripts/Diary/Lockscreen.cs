using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lockscreen : MonoBehaviour
{

    public bool unlocked;
    [SerializeField] GameObject textArea;
    [SerializeField] List<LinePoint> form;

    public void Unlock()
    {
        unlocked = true;
        gameObject.SetActive(false);
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
            Unlock();
            textArea.gameObject.SetActive(true);
        }
    }

    private bool TestForm()
    {
        return DrawManager.pattern.TestForm(form);
    }
}
