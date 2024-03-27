using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FogOfWar : MonoBehaviour
{

    [SerializeField]
    private Tilemap wall_tilemap, fow_tilemap;

    [SerializeField]
    private TileBase fow_tile;

    private void Start()
    {
        fow_tilemap.size = wall_tilemap.size; // makes fow tilemap as large as the whole map

        fow_tilemap.BoxFill(wall_tilemap.cellBounds.min, fow_tile, // paints fow over entire map at start of game
                    wall_tilemap.cellBounds.min.x, wall_tilemap.cellBounds.min.y,
                    wall_tilemap.cellBounds.max.x, wall_tilemap.cellBounds.max.y);
    }

    public void reset_fow(List<Vector2> positions_to_fill) // repaints fog of war
    {
        foreach (Vector2 position in positions_to_fill)
        {
            fow_tilemap.SetTile(fow_tilemap.WorldToCell(position), fow_tile);
        }
    }

    public void clear_fow(List<Vector2> positions_to_clear) // clears fog of war
    {
        foreach (Vector2 position in positions_to_clear)
        {
            fow_tilemap.SetTile(fow_tilemap.WorldToCell(position), null);
        }
    }
}
