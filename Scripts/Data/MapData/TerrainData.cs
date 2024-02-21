using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName ="Map/Data")]

public class TerrainData : ScriptableObject
{

    public bool walkable;
    public int movement_cost = 1;

}
