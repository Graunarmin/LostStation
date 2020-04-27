using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointerControl : MonoBehaviour
{
    private void Update()
    {
        transform.position = Reference.instance.camera2D.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, 100f);
    }
}
