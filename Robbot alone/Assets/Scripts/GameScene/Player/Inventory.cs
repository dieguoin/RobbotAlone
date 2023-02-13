using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private static Inventory instance;


    public List<InGameObjects> bodyParts = new List<InGameObjects>(5); //partes del cuerpo llebadas al juego

    public List<Module> modules = new List<Module>(3); //se ordena en escena de inventario y se lleva al juego

    public List<InGameObjects> objectsBackPack = new List<InGameObjects>(); //Solo para la escena de inventario
    public List<Module> modulesBackPacK = new List<Module>(); //Solo para la escena de inventario
    // Start is called before the first frame update
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

    public void AddModule(Module module)
    {
        modulesBackPacK.Add(module);
    }
}
