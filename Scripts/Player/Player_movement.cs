using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_movement : MonoBehaviour
{

    private Rigidbody rig;
    public float speed = 1f;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        Vector3 m_input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        rig.MovePosition(transform.position + m_input * Time.deltaTime * speed);
    }


}