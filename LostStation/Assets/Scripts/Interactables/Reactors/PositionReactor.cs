using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionReactor : StateReactor
{
    public float changePosX;
    public float changePosY;
    public float changePosZ;

    Transform transf;
    Vector3 startPos;

    protected override void Awake()
    {
        transf = GetComponent<Transform>();
        startPos = transf.position;
        base.Awake();
        //So the base Color is always the inactive color (or the active one)
        React();
    }

    public override void React()
    {
        if (switcher.state)
        {
            transf.position = new Vector3 (startPos.x - changePosX,startPos.y - changePosY,startPos.z - changePosZ);

        }
        else
        {
            transf.position = startPos;
        }
    }
}
