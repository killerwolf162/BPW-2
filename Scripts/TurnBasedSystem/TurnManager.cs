using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TurnManager : MonoBehaviour
{
    Queue<EnemyTurnTaker> enemy_queue = new Queue<EnemyTurnTaker>();

    public UnityEvent on_block_player_input, on_unblock_player_input;

    public void next_turn()
    {
        Debug.Log("Waiting..");
        on_block_player_input?.Invoke();

        enemy_turn();
    }

    private void enemy_turn()
    {
        enemy_queue = new Queue<EnemyTurnTaker>(FindObjectsOfType<EnemyTurnTaker>());
        StartCoroutine(enemy_take_turn(enemy_queue));
    }

    private IEnumerator enemy_take_turn(Queue<EnemyTurnTaker> enemy_queue)
    {
        while (enemy_queue.Count > 0)
        {
            EnemyTurnTaker turn_taker = enemy_queue.Dequeue();
            turn_taker.take_turn();
            yield return new WaitUntil(turn_taker.is_finished);
            turn_taker.Reset();
        }
        Debug.Log("Player turn begins");
        player_turn();
    }

    private void player_turn()
    {
        
        foreach (TurnTaker turn_taker in FindObjectsOfType<TurnTaker>())
        {
            turn_taker.wait_turn();
            Debug.Log($"Unit {turn_taker.name} is waiting");
        }
        Debug.Log("New turn is ready!");
        on_unblock_player_input?.Invoke();
    }
}

public interface ITurnDependant
{
    void wait_turn();
}
