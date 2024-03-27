using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVision : MonoBehaviour
{

    public static FogOfWar fow;
    public int range = 10;

    private Unit unit;

    private void Start()
    {
        unit = GetComponent<Unit>();
        if (fow == null)
            fow = FindObjectOfType<FogOfWar>();
        reset_positions();
        unit.OnMove += reset_positions; // calls reset FOW everytime onMove is called
    }

    public void reset_positions() // clears and re-paints FOW
    {
        List<Vector2> positions_to_clear = calculate_positions_around();
        List<Vector2> positions_to_fill = calculate_positions_away();
        fow.clear_fow(positions_to_clear);
        fow.reset_fow(positions_to_fill);
    }

    private List<Vector2> calculate_positions_away() // calculates all positions further than 10 away
    {
        List<Vector2> positions = new List<Vector2>();
        Vector2 center_position = new Vector2(transform.position.x, transform.position.y);

        for (int y = -(range + 1); y <= (range + 1); y++)
        {
            for (int x = -(range + 1); x <= (range + 1); x++)
            {
                Vector2 temp_positions = center_position + new Vector2(x, y);
                if (Vector2.Distance(center_position,temp_positions) > range)
                {
                    positions.Add(temp_positions);
                }
            }
        }
        return positions;
    }


    // make it so that this takes into account that it doesnt reveal map behind walls, no clue how :)
    private List<Vector2> calculate_positions_around() // calculates all positions in a 10 unit radius
    {
        List<Vector2> positions = new List<Vector2>();
        Vector2 center_position = new Vector2(transform.position.x, transform.position.y);

        for (int y = -range; y <= range; y++)
        {
            for (int x = -range; x <= range; x++)
            {
                Vector2 temp_positions = center_position + new Vector2(x, y);
                if (Vector2.Distance(center_position,temp_positions) <= range)
                {
                    positions.Add(temp_positions);
                }
            }
        }
        return positions;
    }
}
