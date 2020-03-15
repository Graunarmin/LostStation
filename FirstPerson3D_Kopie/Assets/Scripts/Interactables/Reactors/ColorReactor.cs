using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class ColorReactor : StateReactor
{
    public Color active;
    public Color inactive;

    MeshRenderer mesh;

    protected override void Awake()
    { 
        mesh = GetComponent<MeshRenderer>();
        base.Awake();
        //So the base Color is always the inactive color (or the active one)
        React();
    }

    public override void React()
    {
        if (switcher.state){
            mesh.material.color = active;

        }else{
            mesh.material.color = inactive;
        }
    }
}
