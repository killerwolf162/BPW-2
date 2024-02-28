using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_movement : MonoBehaviour
{

    [SerializeField]
    private Map map;


    public float treshold = 0.5f;

    private Unit selected_unit;

    public void handle_movement(Vector3 end_position)
    {
        if (this.selected_unit == null)
            return;

        if (this.selected_unit.can_still_move() == false)
            return;

        if (Vector2.Distance(end_position, this.selected_unit.transform.position) > treshold)
        {
            Vector2 direction = calculate_movement(end_position);

            if(map.can_i_move_to((Vector2)this.selected_unit.transform.position, direction))
            {
                this.selected_unit.handle_movement(direction, 1);
            }
            else
            {
                Debug.Log($"cant move in direction {direction}");
            }

            
        }
    }

    private Vector2 calculate_movement(Vector3 end_position)
    {
        Vector2 direction = (end_position - this.selected_unit.transform.position);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            float sign = Mathf.Sign(direction.x);
            direction = Vector2.right * sign;
        }
        else
        {
            float sign = Mathf.Sign(direction.y);
            direction = Vector2.up * sign;
        }

        return direction;
    }

    public void handle_selection(GameObject detected_object)
    {
        if (detected_object == null)
            return;

        this.selected_unit = detected_object.GetComponent<Unit>();
        Dictionary<Vector2Int, Vector2Int?> result
            = map.get_movement_range(this.selected_unit.transform.position, this.selected_unit.Current_movement_points);

        foreach (Vector2Int position in result.Keys)
        {
            Debug.Log(position);
        }
    }
}