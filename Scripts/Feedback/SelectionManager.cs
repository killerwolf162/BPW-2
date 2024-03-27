using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour, ITurnDependant
{

    FlashFeedback flash_feed_back;

    public void handle_selection(GameObject detected_colider)
    {
        deselected_old_object();

        if (detected_colider == null)
            return;

        flash_feed_back = detected_colider.GetComponent<FlashFeedback>();
        if (flash_feed_back != null)
            flash_feed_back.play_feedback();
    }

    public void wait_turn() // deselects player when starting nect turn
    {
        deselected_old_object();
    }

    public void deselected_old_object() // makes player stop flashing when not selected
    {
        if (flash_feed_back == null)
            return;
        flash_feed_back.stop_feedback();
        flash_feed_back = null;
    }
}
