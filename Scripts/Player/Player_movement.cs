using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_movement : MonoBehaviour
{

    private Rigidbody rig;
    private Animator animator;
    public float speed = 1f;
    Vector3 player_pos;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Wall"))
                Debug.Log("collided");
        }

        if (Input.GetKeyDown(KeyCode.D))
            move_right();
        if (Input.GetKeyDown(KeyCode.A))
            move_left();
        if (Input.GetKeyDown(KeyCode.W))
            move_forward();
        if (Input.GetKeyDown(KeyCode.S))
            move_backward();

        /*if (Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0 || Input.GetAxis("Vertical") > 0 || Input.GetAxis("Vertical") < 0)
            animator.SetBool("IsWalking", true);
        else
            animator.SetBool("IsWalking", false);
        */
    }

    public void move_right()
    {
        rig.MovePosition(transform.position + new Vector3(1, 0, 0));
        void OnTriggerEnter(Collider other)
        {

            //if (other.gameObject.CompareTag("Wall"))
              //  rig.MovePosition(transform.position + new Vector3(-1, 0, 0));
        }

    }

    public void move_left()
    {
        rig.MovePosition(transform.position + new Vector3(-1, 0, 0));

        void OnTriggerEnter(Collider other)
        {
           // if (other.gameObject.CompareTag("Wall"))
               // rig.MovePosition(transform.position + new Vector3(1, 0, 0));
        }
    }

    public void move_forward()
    {
        rig.MovePosition(transform.position + new Vector3(0, 0, 1));

        void OnTriggerEnter(Collider other)
        {
           // if (other.gameObject.CompareTag("Wall"))
              //  rig.MovePosition(transform.position + new Vector3(0, 0, -1));
        }
    }

    public void move_backward()
    {
        rig.MovePosition(transform.position + new Vector3(0, 0, -1));

        void OnTriggerEnter(Collider other)
        {
           // if (other.gameObject.CompareTag("Wall"))
             //   rig.MovePosition(transform.position + new Vector3(0, 0, 1));
        }
    }

}