using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(AbstractDungeonGenerator), true)]
public class RGD_editor : Editor
{


    AbstractDungeonGenerator generator;

    private void Awake()
    {
        generator = (AbstractDungeonGenerator)target;
        
    }

    public override void OnInspectorGUI()
    {

        if(GUILayout.Button("Create Dungeon"))
        {
            generator.generate_dungeon();
        }

        if (GUILayout.Button("Clear Dungeon"))
        {
            generator.clear_dungeon();
        }
    }



}
