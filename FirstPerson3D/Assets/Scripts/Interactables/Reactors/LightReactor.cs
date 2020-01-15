using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class LightReactor : StateReactor
{

    protected override void Awake(){
        base.Awake();
        //So the base Color is always the inactive color (or the active one)
        React();
    }

    public override void React(){

        //when switch is turned on (true): switch on Light
        if (switcher.state){
            Debug.Log("Switched Lights on!");
            gameObject.SetActive(true);
        }
        //else switch lights off
        else{
            gameObject.SetActive(false);
        }
    }
}
