using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGrid
{

    Dictionary<Vector2Int, TerrainData> grid = new Dictionary<Vector2Int, TerrainData>();

    public void add_to_grid(TerrainData terrain_type, List<Vector2Int> collection)
    {
        foreach (Vector2Int cell in collection)
        {
            grid[cell] = terrain_type;
        }
    }

    public readonly static List<Vector2Int> neighbours_4_directions = new List<Vector2Int>
    {
        new Vector2Int(0,1), new Vector2Int(1,0), new Vector2Int(-1,0), new Vector2Int(0,-1)
    };

    public bool check_if_positiion_is_valid(Vector2Int int_position)
    {
        return grid.ContainsKey(int_position) && grid[int_position].walkable; 
    }

    public int get_movement_cost(Vector2Int tile_world_position)
    {
        return grid[tile_world_position].movement_cost;
    }

    public List<Vector2Int> get_neighbours_for(Vector2Int tile_world_position)
    {
        List<Vector2Int> positions = new List<Vector2Int>();

        foreach (Vector2Int direction in neighbours_4_directions)
        {
            Vector2Int temp_position = tile_world_position + direction;
            if (grid.ContainsKey(temp_position))
                positions.Add(temp_position);
        }
        return positions;
    }




}
