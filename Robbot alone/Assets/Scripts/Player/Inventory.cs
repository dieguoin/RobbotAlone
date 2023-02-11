using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private Dictionary<string, bool> objectsInventory = new Dictionary<string, bool>();
    // Start is called before the first frame update
    private void Start()
    {
        objectsInventory.Add("Gun", false);
        objectsInventory.Add("JetPack", false);
    }
    public void AddObject(string key)
    {
        objectsInventory[key] = true;
    }
    public bool HasObject(string key)
    {
        return objectsInventory[key];
    }
    
}
