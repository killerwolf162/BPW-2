using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Map/Data")]

public class TerrainData : ScriptableObject
{

    public bool walkable; // if you can walk over terrain
    public int movement_cost = 1; // how many points it costs to walk over it

}
