using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InGameObjects> bodyParts = new List<InGameObjects>(5); //partes del cuerpo llebadas al juego
    public List<GameObject> objectsInventory = new List<GameObject>(); //se ordena en escena de inventario y se lleva al juego
    public List<GameObject> objectsBackPack = new List<GameObject>(); //Solo para la escena de inventario
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
