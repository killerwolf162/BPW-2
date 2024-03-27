using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    [SerializeField]
    private GameObject enemy1, enemy2;

    private List<GameObject> enemy1_list;
    private List<GameObject> enemy2_list;

    [SerializeField]
    public int amount_of_enemy1, amount_of_enemy2, amount_of_spawn_locations;

    private Map map;

    List<Vector2Int> possible_spawn_locations;

    private void Awake()
    {
        map = FindObjectOfType<Map>();
 
    }

    private void Start()
    {
        enemy1_list = add_enemies_to_list(enemy1, amount_of_enemy1); // makes list of first type of enemy
        enemy2_list = add_enemies_to_list(enemy2, amount_of_enemy2); // makes list of second type of enemy
        possible_spawn_locations = add_spawn_locations(map.get_world_position_tiles_from(map.floor_tilemap), amount_of_spawn_locations); // uses all tile positions in floor tilemap for possible spawn locations
        spawn_enemies();
    }

    private List<GameObject> add_enemies_to_list(GameObject enemy, int count)
    {
        List<GameObject> temp_list = new List<GameObject>();
        for (int i = 0; i <= count; i++)
        {
            temp_list.Add(enemy);
        }
        return temp_list;
    }

    private void spawn_enemies() // spawn enemys
    {
        

        foreach (GameObject enemy in enemy1_list)
        {
            create_enemy(enemy); 
        }

        foreach (GameObject enemy in enemy2_list)
        {
            create_enemy(enemy);
        }


    }

    private void create_enemy(GameObject enemy)
    {
        int index;
        Vector3Int spawn_location;

        generate_random_spawn_location(out index, out spawn_location);

        Instantiate(enemy, spawn_location + new Vector3(0.5f, 0.5f), transform.rotation);

        possible_spawn_locations.RemoveAt(index);// removes spawn location from list of possible spawn locations
    }

    private void generate_random_spawn_location(out int index, out Vector3Int spawn_location) // picks random spot on map to set as spawn location
    {
        index = UnityEngine.Random.Range(0, possible_spawn_locations.Count);
        spawn_location = new Vector3Int();
        spawn_location = (Vector3Int)possible_spawn_locations[index];
    }

    private List<Vector2Int> add_spawn_locations(List<Vector2Int> input_list, int count) // creates list with possible spawn locations
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
