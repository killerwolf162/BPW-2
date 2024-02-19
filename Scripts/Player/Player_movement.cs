using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_movement : MonoBehaviour
{

    public float treshold = 0.5f;

    private GameObject selected_object;

    private FlashFeedback flash_feedback;

    public void handle_movement(Vector3 end_position)
    {
        if (selected_object == null)
            return;

        flash_feedback.stop_feedback();
        
        if (Vector2.Distance(end_position, selected_object.transform.position) > treshold)
        {
            Vector2 direction = (end_position - selected_object.transform.position);
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
            selected_object.transform.position += (Vector3)direction;
        }
    }

    public void handle_selection(GameObject detected_object)
    {
        if (detected_object != null)
        {
            this.selected_object = detected_object;
            flash_feedback = selected_object.GetComponent<FlashFeedback>();
            flash_feedback.play_feedback();
        }
    }
}