using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{

    [SerializeField]
    protected TileMapVisualizer tile_map_visualizer = null;
    [SerializeField]
    protected Vector2Int start_position = Vector2Int.zero;

    public void generate_dungeon()
    {
        tile_map_visualizer.Clear();
        generate();
    }

    protected abstract void generate();
}
