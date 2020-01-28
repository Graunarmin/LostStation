using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : Item
{
    public Switcher observedSwitcher;

    public GameObject lights;
    public GameObject lamps;

    private void Awake()
    {
        lights.gameObject.SetActive(false);
        lamps.gameObject.SetActive(false);
        observedSwitcher.Change += SwitchOnLights;
    }

    private void SwitchOnLights()
    {
        lights.gameObject.SetActive(true);
        lamps.gameObject.SetActive(true);
    }
}
