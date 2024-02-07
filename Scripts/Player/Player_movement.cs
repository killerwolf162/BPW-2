using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_movement : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            move_forward();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            move_backward();
        }
        else if ( Input.GetKeyDown(KeyCode.A))
        {
            move_left();
        }
        else if ( Input.GetKeyDown(KeyCode.D))
        {
            move_right();
        }

    }


    private void move_right()
    {
        transform.position = transform.position + new Vector3(1, 0, 0);
    }

    private void move_left()
    {
        transform.position = transform.position + new Vector3(-1, 0, 0);
    }

    private void move_forward()
    {
        transform.position = transform.position + new Vector3(0, 0, 1);
    }

    private void move_backward()
    {
        transform.position = transform.position + new Vector3(-0, 0, -1);
    }



}