using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{ 
    public List<GameObject> objectsInventory = new List<GameObject>();
    public List<GameObject> objectsBackPack = new List<GameObject>();
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void AddObject(GameObject newObject)
    {
        objectsBackPack.Add(newObject);
    }
}
