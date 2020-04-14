using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DissolveScript : MonoBehaviour
{
    // Start is called before the first frame update
public Material dissolveMat;
public float health;
public float maxHealth;
public GameObject object1;
public GameObject object2;

void Start() {


}

void Update()
{
dissolveMat.SetFloat("_V1",health);
    float distance = Vector3.Distance (object1.transform.position, object2.transform.position);
health = distance.Remap(2,10,0,1);
if(health>1) health=1;
if(health<0) health=0;
print(distance);

}


}
