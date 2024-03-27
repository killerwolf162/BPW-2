using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapEnemy : MonoBehaviour
{
    // was trying to create a way to swap enemy position when they would walk into an ally
    // this was to make it so that they could always complete their path. Never got around to finishing it

    private Vector3 current_position;
    private Vector3 enemy_position;
    private void Awake()
    {
       
    }

    void OnTriggerStay2D(Collider2D other)
    {

        Debug.Log("collided");
        enemy_position = other.gameObject.transform.position;
        current_position = this.gameObject.transform.position;

        other.gameObject.transform.position = current_position;
        this.gameObject.transform.position = enemy_position;
    }

}
