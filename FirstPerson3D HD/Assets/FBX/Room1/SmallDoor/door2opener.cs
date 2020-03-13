using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door2opener : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
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
            if (Input.GetMouseButtonDown(0))
            {
                anim.Play("2 open");
                what2 = 1;
            }
        }

        if (what == false)
        {
            if (what2 == 1)
            {
                anim.Play("2 close");
                what2 = 2;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        
        what = true;


    }

    private void OnTriggerStay(Collider other)
    {
        what = true;

    }


    private void OnTriggerExit(Collider other)
    {
        what = false;

        

    }
}
