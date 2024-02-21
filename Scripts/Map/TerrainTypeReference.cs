using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainTypeReference : MonoBehaviour
{

    [SerializeField]
    private TerrainData terrain_data;

    public TerrainData get_terrain_data() => terrain_data;

}
