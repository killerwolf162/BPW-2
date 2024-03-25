using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class RandomWalkGenerator : AbstractDungeonGenerator
{

    [SerializeField]
    private int iterations = 10;
    [SerializeField]
    public int walk_length = 10;
    [SerializeField]
    public bool start_randomly_each_iteration = true;

    private void Awake()
    {
        generate();
    }

    protected override void generate()
    {
        HashSet<Vector2Int> floor_positions = run_random_walk();
        tile_map_visualizer.Clear();
        tile_map_visualizer.paint_floor_tiles(floor_positions);
        WallGenerator.create_walls(floor_positions, tile_map_visualizer);
    }

    protected HashSet<Vector2Int> run_random_walk()
    {
        var current_position = start_position;
        HashSet<Vector2Int> floor_positions = new HashSet<Vector2Int>();

        for (int i = 0; i < iterations; i++)
        {
            var path = PG_Algorithems.simple_random_walk(current_position, walk_length);
            floor_positions.UnionWith(path);

            if (start_randomly_each_iteration)
                current_position = floor_positions.ElementAt(Random.Range(0, floor_positions.Count));
        }
        return floor_positions;
    }
}
