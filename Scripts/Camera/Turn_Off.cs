using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn_Off : MonoBehaviour
{

    public GameObject camera;
    
    void Start()
    {
        camera.GetComponent<Camera>().enabled = false;
    }
}
