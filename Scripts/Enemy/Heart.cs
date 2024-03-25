using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    ScoreManager score_manager;
    Health player_health;

    private void Awake()
    {
        score_manager = GameObject.Find("Canvas").GetComponent<ScoreManager>();
        player_health = GameObject.FindWithTag("Player").GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            score_manager.increase_money();
            player_health.current_health = player_health.max_health;
        }
    }
}
