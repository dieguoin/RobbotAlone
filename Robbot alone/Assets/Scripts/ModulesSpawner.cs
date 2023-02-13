using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    //public Module [] modules;
    public GameObject[] modules;
    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int randModule = Random.Range(0, modules.Length);
           // int randType = Random.Range(0, 3);
            GameObject Go = transform.GetChild(i).gameObject;
            Instantiate(modules[randModule], Go.transform, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
/*
public class Module
{
    public string name;
    public int type;
    public Sprite sprite;
}
*/