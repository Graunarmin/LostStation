using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Observer Pattern: Observer
public class Enemy : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
        Player.onEnemyHit += Damage;
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    void Damage(Color color)
	{
        transform.GetComponent<Renderer>().material.color = color;
	}

    //unsubscribe destroyed objects
    private void OnDisable()
    {
        Player.onEnemyHit -= Damage;
    }
}
