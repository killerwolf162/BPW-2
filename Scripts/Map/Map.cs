using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{

    [SerializeField]
    private Tilemap floor_tilemap, wall_tilemap;

    private List<Vector2Int> floor_tiles, wall_tiles;

    [SerializeField]
    private bool show_floor, show_wall;

    private MapGrid map_grid;


    private void Awake()
    {
        floor_tiles = get_world_position_tiles_from(floor_tilemap);
        wall_tiles = get_world_position_tiles_from(wall_tilemap);

        prepare_map_grid();
    }

    private void prepare_map_grid()
    {
        map_grid = new MapGrid();
        map_grid.add_to_grid(floor_tilemap.GetComponent<TerrainTypeReference>().get_terrain_data(), floor_tiles);
       // map_grid.add_to_grid(wall_tilemap.GetComponent<TerrainTypeReference>().get_terrain_data(), wall_tiles);
    }

    private List<Vector2Int> get_world_position_tiles_from(Tilemap tilemap)
    {
        List<Vector2Int> temp_list = new List<Vector2Int>();
        foreach (Vector2Int cell_position in tilemap.cellBounds.allPositionsWithin)
        {
            
            Vector3Int world_position = get_world_position(cell_position);

            if (tilemap.HasTile((Vector3Int)cell_position))
                temp_list.Add((Vector2Int)world_position);
        }
        return temp_list;
    }

    public bool can_i_move_to(Vector2 unit_position, Vector2 direction)
    {
        Vector2Int unit_tile_position = Vector2Int.FloorToInt(unit_position + direction);

        List<Vector2Int> neighbours = map_grid.get_neighbours_for(Vector2Int.FloorToInt(unit_position));

        foreach( Vector2Int cell_position in neighbours)
        {
            Debug.Log(cell_position);
        }


        return neighbours.Contains(unit_tile_position); 
    }

    private Vector3Int get_world_position(Vector2Int cell_position)
    {
        return Vector3Int.CeilToInt(floor_tilemap.CellToWorld((Vector3Int)cell_position));
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
            return;
        DrawGizomOf(wall_tiles, Color.white, show_wall);
        DrawGizomOf(floor_tiles, Color.red, show_floor);
    }

    private void DrawGizomOf(List<Vector2Int> tiles, Color color, bool is_showing)
    {
        if (is_showing)
        {
            Gizmos.color = color;
            foreach (Vector2Int pos in tiles)
            {
                Gizmos.DrawSphere(new Vector3(pos.x + 0.5f, pos.y + 0.5f, 0), 0.3f);
            }
        }
    }




}
