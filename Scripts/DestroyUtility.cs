using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyUtility : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer sprite_renderer;

    public void destroy_object()
    {
        Destroy(gameObject);
    }

    public void disable_renderer()
    {
        sprite_renderer = GetComponent<SpriteRenderer>();
        sprite_renderer.enabled = false;
    }

}
