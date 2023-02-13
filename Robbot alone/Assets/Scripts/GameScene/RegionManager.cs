using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionManager : MonoBehaviour
{
    public GameObject mapSpawnerPrefab;
    public GameObject playerSpawnPoint;
    public GameObject playerExtractionPoint;
    public GameObject floorPrefab;
    public GameObject stairsPrefab;
    public GameObject WallPrefab;
    public GameObject ceilingPrefab;
    public GameObject floorDoorPrefab;
    public GameObject damageDoorPrefab;
    public GameObject buttonPrefab;
    public GameObject enemy1Prefab;
    public GameObject enemy2Prefab;
    public GameObject enemy3Prefab;

    public void SpawnElement(GameObject element)
    {
        GameObject newElement = GameObject.Instantiate(element, transform);
        newElement.transform.localPosition = new Vector3(0, 0);
    }
}
