using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PG_Algorithems
{
 
    public static HashSet<Vector2Int> simple_random_walk(Vector2Int start_position, int walk_lenght)
    {
        HashSet<Vector2Int> path = new HashSet<Vector2Int>();

        path.Add(start_position);
        var previous_position = start_position;

        for (int i = 0; i < walk_lenght; i++)
        {
            var new_position = previous_position + Direction2D.get_random_cardinal_direction();
            path.Add(new_position);
            previous_position = new_position;
        }
        return path;

    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinal_directions_list = new List<Vector2Int>
    {
        new Vector2Int(0,1), // up
        new Vector2Int(1,0), // right
        new Vector2Int(0,-1), // down
        new Vector2Int(-1,0) // left
    };

    public static Vector2Int get_random_cardinal_direction()
    {
        return cardinal_directions_list[Random.Range(0, cardinal_directions_list.Count)];
    }

}
