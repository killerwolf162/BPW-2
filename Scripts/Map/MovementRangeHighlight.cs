using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovementRangeHighlight : MonoBehaviour
{

    [SerializeField]
    private Tilemap highlight_tilemap;
    [SerializeField]
    private TileBase highlight_tile;


    public void clear_highlight()
    {
        highlight_tilemap.ClearAllTiles();
    }

    public void highlight_tiles(IEnumerable<Vector2Int> cell_positions)
    {
        clear_highlight();
        foreach(Vector2Int tile_position in cell_positions)
        {
            highlight_tilemap.SetTile((Vector3Int)tile_position, highlight_tile);
        }
    }    




}
