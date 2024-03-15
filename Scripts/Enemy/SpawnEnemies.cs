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
        enemy1_list = add_enemies_to_list(enemy1, amount_of_enemy1);
        enemy2_list = add_enemies_to_list(enemy2, amount_of_enemy2);
        possible_spawn_locations = add_spawn_locations(map.get_world_position_tiles_from(map.floor_tilemap), amount_of_spawn_locations);
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

    private void spawn_enemies()
    {
        foreach (GameObject enemy in enemy1_list)
        {
            int index = UnityEngine.Random.Range(0, possible_spawn_locations.Count);
            Vector3Int spawn_location = new Vector3Int();
            spawn_location = (Vector3Int)possible_spawn_locations[index];

            Instantiate(enemy, spawn_location + new Vector3(0.5f, 0.5f), transform.rotation);

            possible_spawn_locations.RemoveAt(index);
        }

        foreach (GameObject enemy in enemy2_list)
        {
            int index = UnityEngine.Random.Range(0, possible_spawn_locations.Count);
            Vector3Int spawn_location = new Vector3Int();
            spawn_location = (Vector3Int)possible_spawn_locations[index];

            Instantiate(enemy, spawn_location + new Vector3(0.5f, 0.5f), transform.rotation);

            possible_spawn_locations.RemoveAt(index);
        }
    }

    //private List<Vector2Int> add_spawn_locations(List<Vector2Int> input_list, int count)
    //{
    //    for(int i = 0; i <= count; i++)
    //    {
    //        int index = UnityEngine.Random.Range(0, input_list.Count);
    //        possible_spawn_locations.Add(input_list[index]);
    //    }

    //    return possible_spawn_locations;
    //}

    private List<Vector2Int> add_spawn_locations(List<Vector2Int> input_list, int count)
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
