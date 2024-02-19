using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFeedback : MonoBehaviour
{
    public SpriteRenderer sprite_renderer;
    [SerializeField]
    private float invisable_time, visable_time;

    public void play_feedback()
    {
        if (sprite_renderer == null)
            return;
        stop_feedback();
        StartCoroutine(flash_coroutine());
    }

    private IEnumerator flash_coroutine()
    {
        Color sprite_color = sprite_renderer.color;
        sprite_color.a = 0;
        sprite_renderer.color = sprite_color;
        yield return new WaitForSeconds(invisable_time);

        sprite_color.a = 1;
        sprite_renderer.color = sprite_color;
        yield return new WaitForSeconds(visable_time);
        StartCoroutine(flash_coroutine());
    }

    public void stop_feedback()
    {
        StopAllCoroutines();
        Color sprite_color = sprite_renderer.color;
        sprite_color.a = 1;
        sprite_renderer.color = sprite_color;
    }
}
