using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InGameObjects> bodyParts = new List<InGameObjects>(5); //partes del cuerpo llebadas al juego
    public List<InGameObjects> objectsInventory = new List<InGameObjects>(); //se ordena en escena de inventario y se lleva al juego
    public List<InGameObjects> objectsBackPack = new List<InGameObjects>(); //Solo para la escena de inventario
    // Start is called before the first frame update
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void AddObject(InGameObjects newObject)
    {
        objectsBackPack.Add(newObject);
    }
}
