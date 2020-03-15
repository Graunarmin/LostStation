using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Item
{
    public Switcher observedSwitcher;

    public GameObject lights;
    public GameObject lamps;
    public GameObject roofON;
    public GameObject roofOFF;
    private bool active;

    private void Awake()
    {
       
        gameObject.GetComponent<LightSwitch>().SwitchOffLights();

        //but lets switch on the lights for testing!
        lights.gameObject.SetActive(true);
        lamps.gameObject.SetActive(true);

        //roofON.gameObject.SetActive(false);
        //roofOFF.gameObject.SetActive(true);

        //observedSwitcher.Change += SwitchOnLights;
        observedSwitcher.Change += GeneratorActive;
    }

    //private void SwitchOnLights()
    //{
    //    lights.gameObject.SetActive(true);
    //    lamps.gameObject.SetActive(true);
    //    roofON.gameObject.SetActive(true);
    //    roofOFF.gameObject.SetActive(false);

    //}

    private void GeneratorActive()
    {
        active = true;
    }

    public bool isActive()
    {
        return active;
    }
}
