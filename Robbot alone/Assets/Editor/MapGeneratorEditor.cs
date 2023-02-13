using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(RegionManager))]
public class MapGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        RegionManager myRegionManager = (RegionManager)target;
        if (GUILayout.Button("New Spawner"))
        {
            myRegionManager.SpawnElement(myRegionManager.mapSpawnerPrefab);
        }
        if(GUILayout.Button("New Floor"))
        {
            myRegionManager.SpawnElement(myRegionManager.floorPrefab);
        }
        if (GUILayout.Button("New Wall"))
        {
            myRegionManager.SpawnElement(myRegionManager.WallPrefab);
        }
        if (GUILayout.Button("New Ceiling"))
        {
            myRegionManager.SpawnElement(myRegionManager.ceilingPrefab);
        }
        if (GUILayout.Button("New Floor Door"))
        {
            myRegionManager.SpawnElement(myRegionManager.floorDoorPrefab);
        }
        if (GUILayout.Button("New Damage Door"))
        {
            myRegionManager.SpawnElement(myRegionManager.damageDoorPrefab);
            myRegionManager.SpawnElement(myRegionManager.buttonPrefab);
        }
        if (GUILayout.Button("New Floor"))
        {
            myRegionManager.SpawnElement(myRegionManager.floorPrefab);
        }
        if (GUILayout.Button("New Enemy1"))
        {
            myRegionManager.SpawnElement(myRegionManager.enemy1Prefab);
        }
        if (GUILayout.Button("New Enemy2"))
        {
            myRegionManager.SpawnElement(myRegionManager.enemy2Prefab);
        }
        if (GUILayout.Button("New Enemy3"))
        {
            myRegionManager.SpawnElement(myRegionManager.enemy3Prefab);
        }
        if (GUILayout.Button("New Player Spawn"))
        {
            myRegionManager.SpawnElement(myRegionManager.playerSpawnPoint);
        }
        if (GUILayout.Button("New Player Estraction"))
        {
            myRegionManager.SpawnElement(myRegionManager.playerExtractionPoint);
        }
    }
}
