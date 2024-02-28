using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    void Awake()
    {
        player = GameObject.Find("Player(Clone)");
    }

    void Update()
    {
        transform.position = player.transform.position;
    }
}
