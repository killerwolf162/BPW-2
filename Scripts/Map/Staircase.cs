using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Staircase : MonoBehaviour
{


    GameObject next_level_pop_up;

    private void move_to_next_level()
    {
        next_level_pop_up.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            move_to_next_level();
        }
    }


}
