using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class walker : MonoBehaviour
{
    public Animator Anim;
    // Start is called before the first frame update
    void Start()
    {
        Anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("w"))
        {
            Anim.SetBool("isWalk", true);
        }
        else
        {
            Anim.SetBool("isWalk", false);

        }

        if (Input.GetKey("w") && Input.GetKey("left shift"))
        {
            
            Anim.SetBool("isRunning", true);
        }
        else
        {
            Anim.SetBool("isRunning", false);

        }
    }
}
