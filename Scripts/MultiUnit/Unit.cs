using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour, ITurnDependant
{


    [SerializeField]
    private int max_movement_points = 2;

    [SerializeField]
    private int current_movement_points;


    void Start()
    {
        reset_movement_points();
    }

    private void reset_movement_points()
    {
        current_movement_points = max_movement_points;
    }

    public bool can_still_move()
    {
        return current_movement_points > 0;
    }

    public void wait_turn()
    {
        reset_movement_points();
    }

    public void handle_movement(Vector3 cardinal_direction, int move_cost)
    {
        current_movement_points -= move_cost;
        transform.position += (Vector3)cardinal_direction;
    }

}
