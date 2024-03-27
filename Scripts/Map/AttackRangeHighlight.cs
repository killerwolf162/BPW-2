using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AttackRangeHighlight : MonoBehaviour
{
    [SerializeField]
    private Tilemap attack_highlight_tilemap;
    [SerializeField]
    private TileBase attack_highlight_tile;


    public void clear_attack_highlight()
    {
        attack_highlight_tilemap.ClearAllTiles(); // clears attack highlight
    }

    public void highlight_attack_tiles(IEnumerable<Vector2Int> cell_positions) // creates a highlight for the areas the player can attack
    {
        clear_attack_highlight();
        foreach (Vector2Int tile_position in cell_positions)
        {
            attack_highlight_tilemap.SetTile((Vector3Int)tile_position, attack_highlight_tile);
        }
    }
}
