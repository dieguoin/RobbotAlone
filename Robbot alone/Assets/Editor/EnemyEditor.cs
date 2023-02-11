using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        
        Enemy myEnemy = (Enemy) target;
        if(GUILayout.Button("New Point"))
        {
            myEnemy.GeneratePoint();
        }
    }
}
