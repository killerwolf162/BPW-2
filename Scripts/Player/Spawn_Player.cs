using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Player : MonoBehaviour
{

    public GameObject Player;
    public GameObject Start_location;


    void Start()
    {
        Start_location = GameObject.FindGameObjectWithTag("Floor");
        Spawn_player();
    }

    void Spawn_player()
    {
        Instantiate(Player, Start_location.transform.position, Start_location.transform.rotation);
    }

}
