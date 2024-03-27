using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurnTaker : MonoBehaviour
{

    private bool turn_finished;

    private IenemyAI enemy_ai;

    private void Start()
    {
        enemy_ai = GetComponent<IenemyAI>();
        enemy_ai.turn_finished += () => turn_finished = true;
    }

    public void take_turn() // makes player enemy units take their turn
    {
        enemy_ai.start_turn(); ;
    }

    public bool is_finished() => turn_finished;

    public void Reset()
    {
        turn_finished = false;
    }

}

public interface IenemyAI
{
    event Action turn_finished;

    void start_turn();
}
