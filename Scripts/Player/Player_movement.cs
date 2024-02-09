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

        if (Input.GetKeyDown(KeyCode.D))
            move_right();
        else if (Input.GetKeyDown(KeyCode.A))
            move_left();
        else if (Input.GetKeyDown(KeyCode.W))
            move_forward();
        else if (Input.GetKeyDown(KeyCode.S))
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
    }

    public void move_left()
    {
        rig.MovePosition(transform.position + new Vector3(-1, 0, 0));
    }

    public void move_forward()
    {
        rig.MovePosition(transform.position + new Vector3(0, 0, 1));
    }

    public void move_backward()
    {
        rig.MovePosition(transform.position + new Vector3(0, 0, -1));
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Wall") && Input.GetKey(KeyCode.D))
            rig.MovePosition(transform.position + new Vector3(-1, 0, 0));
        else if (other.gameObject.CompareTag("Wall") && Input.GetKey(KeyCode.A))
            rig.MovePosition(transform.position + new Vector3(1, 0, 0));
        else if (other.gameObject.CompareTag("Wall") && Input.GetKey(KeyCode.W))
            rig.MovePosition(transform.position + new Vector3(0, 0, -1));
        else if (other.gameObject.CompareTag("Wall") && Input.GetKey(KeyCode.S))
            rig.MovePosition(transform.position + new Vector3(0, 0, 1));
        
    }

}