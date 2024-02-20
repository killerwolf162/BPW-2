using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HandlePlayerInput : MonoBehaviour
{

    public Camera current_camera;
    public LayerMask layer_mask;

    public UnityEvent<GameObject> on_handle_mouse_click;
    public UnityEvent<Vector3> on_handle_mouse_finishing_dragging;

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            handle_mouse_click();
        }
        if(Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            handle_mouse_up();
        }
    }

    private void handle_mouse_up()
    {
        Vector3 mouse_input = get_mouse_position();
        on_handle_mouse_finishing_dragging?.Invoke(mouse_input);
    }

    private void handle_mouse_click() //handle_mouse_click couldnt be used??
    {
        Vector3 mouse_input = get_mouse_position();
        Collider2D colider = Physics2D.OverlapPoint(mouse_input, layer_mask);
        GameObject selected_object = colider == null ? null : colider.gameObject;

        on_handle_mouse_click?.Invoke(selected_object);
    }

    private Vector3 get_mouse_position()
    {
        Vector3 mouse_input = current_camera.ScreenToWorldPoint(Input.mousePosition);
        mouse_input.z = 0f;
        return mouse_input;
    }


}
