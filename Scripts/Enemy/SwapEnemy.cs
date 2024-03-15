using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapEnemy : MonoBehaviour
{

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
