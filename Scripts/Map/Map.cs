using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{

    [SerializeField]
    public Tilemap floor_tilemap, wall_tilemap;

    private List<Vector2Int> floor_tiles, wall_tiles;

    [SerializeField]
    private bool show_floor, show_wall;

    private MapGrid map_grid;


    public Dictionary<Vector2Int, Vector2Int?> get_movement_range(Vector3 world_position, int current_movement_points) // creates movement highlight
    {
        Vector3Int cell_world_position = get_cell_world_position_for(world_position);
        return GraphSearch.BFS(map_grid, (Vector2Int)cell_world_position, current_movement_points);
    }

    public Dictionary<Vector2Int, Vector2Int?> get_attack_range(Vector3 world_position, int attack_range) // creates attack highlight
    {
        Vector3Int cell_world_position = get_cell_world_position_for(world_position);
        return GraphSearch.BFS(map_grid, (Vector2Int)cell_world_position, attack_range);
    }

    private void Start()
    {
        generate_map();
    }

    public void generate_map()
    {
        floor_tiles = get_world_position_tiles_from(floor_tilemap);
        wall_tiles = get_world_position_tiles_from(wall_tilemap);

        prepare_map_grid();
    }

    private void prepare_map_grid()
    {
        map_grid = new MapGrid();
        map_grid.add_to_grid(floor_tilemap.GetComponent<TerrainTypeReference>().get_terrain_data(), floor_tiles);
    }

    public List<Vector2Int> get_world_position_tiles_from(Tilemap tilemap) // makes list with vector2Ints for all tiles in map
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

    public bool can_i_move_to(Vector2 unit_position, Vector2 direction) // check neighbour positions 
    {
        Vector2Int unit_tile_position = Vector2Int.FloorToInt(unit_position + direction);

        List<Vector2Int> neighbours = map_grid.get_neighbours_for(Vector2Int.FloorToInt(unit_position));

        return neighbours.Contains(unit_tile_position); 
    }

    private Vector3Int get_cell_world_position_for(Vector3 world_position) // used for converting Vector3s to usefull Vector2Ints
    {
        return Vector3Int.CeilToInt(floor_tilemap.CellToWorld(floor_tilemap.WorldToCell(world_position)));
    }

    private Vector3Int get_world_position(Vector2Int cell_position) // used for converting Vector3s to usefull Vector2Ints
    {
        return Vector3Int.CeilToInt(floor_tilemap.CellToWorld((Vector3Int)cell_position));
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false)
            return;
        draw_gizom_of(wall_tiles, Color.white, show_wall);
        draw_gizom_of(floor_tiles, Color.red, show_floor);
    }

    private void draw_gizom_of(List<Vector2Int> tiles, Color color, bool is_showing)
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
