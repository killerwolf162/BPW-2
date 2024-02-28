using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_Player : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    private GameObject player_position;

    [SerializeField]
    public GameObject camera;

    void Start()
    {
        Spawn_player();
        player_position = GameObject.FindWithTag("Player");
        camera.transform.SetParent(player_position.transform, true);
    }

    void Spawn_player()
    {
        Instantiate(player, new Vector3(0.5f,0.5f,0), Quaternion.Euler(0,0,0));

    }

}
