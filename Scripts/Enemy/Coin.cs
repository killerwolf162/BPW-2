using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    ScoreManager score_manager;

    private void Awake()
    {
        score_manager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            score_manager.increase_money();
        }
    }
}
