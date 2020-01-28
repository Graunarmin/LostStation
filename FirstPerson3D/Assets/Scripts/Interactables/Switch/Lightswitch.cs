using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightswitch : MonoBehaviour
{
    public GameObject lights;
    public GameObject lamps;

    private void Awake()
    {
        lights.gameObject.SetActive(false);
        lamps.gameObject.SetActive(false);
        GameManager.OnPowerIsBack += SwitchOnLights;
    }

    private void SwitchOnLights()
    {
        lights.gameObject.SetActive(true);
        lamps.gameObject.SetActive(true);
    }


}
