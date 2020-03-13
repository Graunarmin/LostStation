using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class b : MonoBehaviour
{
    Animator anim;
    private bool what;
    private int what2;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        what = false;
        what2 = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (what == true)
        {
            if (Input.GetKeyDown("f"))
            {
                anim.Play("D2 Open");
                what2 = 1;
            }
        }

        if (what == false)
        {
            if (what2 == 1)
            {
                anim.Play("D2 Close");
                what2 = 2;
            }
        }
    }

    private void OnTriggerEnter(Collider other)

    {
        Debug.Log("yo");
        what = true;


    }

    private void OnTriggerStay(Collider other)
    {
        what = true;

        Debug.Log("blabla");
    }


    private void OnTriggerExit(Collider other)
    {
        what = false;

        Debug.Log("no");

    }

}
