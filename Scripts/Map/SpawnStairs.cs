using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnStairs : MonoBehaviour
{
    [SerializeField]
    GameObject staircase;

    List<Vector2Int> possible_spawn_locations;

    private Map map;

    private void Awake()
    {
        map = FindObjectOfType<Map>();

    }

    private void Start()
    {
        possible_spawn_locations = add_spawn_locations(map.get_world_position_tiles_from(map.floor_tilemap), 30); // uses all tile positions in floor tilemap for possible spawn locations
        spawn_exit();
    }

    private void spawn_exit()
    {
        int index;
        Vector3Int spawn_location;

        generate_random_spawn_location(out index, out spawn_location);

        Instantiate(staircase, spawn_location + new Vector3(0.5f, 0.5f), transform.rotation);
    }

    private void generate_random_spawn_location(out int index, out Vector3Int spawn_location) // picks random spot on map to set as spawn location
    {
        index = UnityEngine.Random.Range(0, possible_spawn_locations.Count);
        spawn_location = new Vector3Int();
        spawn_location = (Vector3Int)possible_spawn_locations[index];
    }

    private List<Vector2Int> add_spawn_locations(List<Vector2Int> input_list, int count) // creates list for all possible spawn locations
    {
        List<Vector2Int> output_list = new List<Vector2Int>();
        for (int i = 0; i <= count; i++)
        {
            int index = UnityEngine.Random.Range(0, input_list.Count);
            output_list.Add(input_list[index]);
        }

        return output_list;
    }
}
