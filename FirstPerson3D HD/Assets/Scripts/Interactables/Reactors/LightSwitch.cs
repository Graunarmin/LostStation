using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : StateReactor
{
    //put all lights to be turned ON by this switch in this list
    [SerializeField] List<GameObject> lights = new List<GameObject>();
    //put everything to be turned OFF by this switch in this list
    [SerializeField] List<GameObject> roofOFF = new List<GameObject>();

    private bool lightsAreOff;

    protected override void Awake()
    {
        base.Awake();
        SwitchOffLights();
    }

    public override void React()
    {
        //Light only works if generator is active
        if (Reference.instance.generator.isActive())
        {
            if (switcher.state)
            {
                SwitchOnLights();
            }
            else
            {
                SwitchOffLights();
            }
        }
    }

    private void SwitchOnLights()
    {
        foreach(GameObject light in lights)
        {
            light.gameObject.SetActive(true);
        }
        foreach (GameObject cutout in roofOFF)
        {
            cutout.gameObject.SetActive(false);
        }

        lightsAreOff = false;
    }

    public void SwitchOffLights()
    {
        foreach (GameObject light in lights)
        {
            light.gameObject.SetActive(false);
        }
        foreach (GameObject cutout in roofOFF)
        {
            cutout.gameObject.SetActive(true);
        }

        lightsAreOff = true;
    }

    public bool LightsAreOff()
    {
        return lightsAreOff;
    }
}
