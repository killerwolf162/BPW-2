using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Tile_map_visualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floor_tile_map;
    [SerializeField]
    private TileBase floor_tile;

    public void paint_floor_tiles(IEnumerable<Vector2Int> floor_positions)
    {
        paint_floor_tiles(floor_positions, floor_tile_map, floor_tile);
    }

    private void paint_floor_tiles(IEnumerable<Vector2Int> floor_positions, Tilemap floor_tile_map, TileBase floor_tile)
    {
        foreach (var position in floor_positions)
        {
            paint_tile(floor_tile_map, floor_tile, position);
        }
    }

    private void paint_tile(Tilemap floor_tile_map, TileBase floor_tile, Vector2Int position)
    {
        var tile_position = floor_tile_map.WorldToCell((Vector3Int)position);
        floor_tile_map.SetTile(tile_position, floor_tile);
    }

    public void Clear()
    {
        floor_tile_map.ClearAllTiles();
    }

}
