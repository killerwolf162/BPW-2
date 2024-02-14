using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Player : MonoBehaviour
{

    public GameObject Player;


    void Start()
    {
        Spawn_player();
    }

    void Spawn_player()
    {
        Instantiate(Player, new Vector3(0.5f,0.5f,0), Quaternion.Euler(0,0,0));
    }

}
