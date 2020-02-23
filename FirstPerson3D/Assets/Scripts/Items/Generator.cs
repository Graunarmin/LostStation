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

    


    private void Awake()
    {
        //lets switch on the lights for testing!
        lights.gameObject.SetActive(true);
        lamps.gameObject.SetActive(true);

        roofON.gameObject.SetActive(false);
        roofOFF.gameObject.SetActive(true);

        observedSwitcher.Change += SwitchOnLights;
    }

    private void SwitchOnLights()
    {
        lights.gameObject.SetActive(true);
        lamps.gameObject.SetActive(true);
        roofON.gameObject.SetActive(true);
        roofOFF.gameObject.SetActive(false);

    }
}
