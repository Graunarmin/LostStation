using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particlemodulator : MonoBehaviour
{
    public ParticleSystem particles;
    bool trigger = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (Time.timeSinceLevelLoad > 25f)
        if(Input.GetKeyDown("space"))
        {
            particles.Pause();

            trigger = true;
        }

        if (Input.GetKeyDown("1"))
        {
            trigger = false;
        }

        if (trigger == true)
        {

        }
    }
}
