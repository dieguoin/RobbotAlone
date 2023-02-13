using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;


    public List<InGameObjects> bodyParts = new List<InGameObjects>(8); //partes del cuerpo llebadas al juego


    public List<InGameObjects> objectsBackPack = new List<InGameObjects>(); //Solo para la escena de inventario

    public int[] lvls;
  
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
        lvls = new int[3];
    }
    private void OnDestroy()
    {
        if(instance == this)
        {
            instance = null;
        }
    }
    public void AddObject(InGameObjects newObject)
    {
        objectsBackPack.Add(newObject);
    }
    /*
    public void AddModule(Module module)
    {
        modulesBackPacK.Add(module);
    }*/
}
