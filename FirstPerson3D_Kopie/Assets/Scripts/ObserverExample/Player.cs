using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Observer Pattern: Observee
public class Player : MonoBehaviour
{

    public static Player player;


    private void Awake()
    {
        if(player == null)
        {
            player = this;
        }
    }


    public delegate void ChangeEnemyColor(Color color);
    public static event ChangeEnemyColor onEnemyHit;


    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //has anyone subscribed to onEnemyHit?
            if(onEnemyHit != null)
            {
                onEnemyHit(Color.red);
            }
        }
        
    }
}
