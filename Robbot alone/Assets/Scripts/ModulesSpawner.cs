using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulesSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    //public Module [] modules;
    public GameObject[] modulesLvl1;
    public GameObject[] modulesLvl2;
    public GameObject[] modulesLvl3;

    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            int type = 0;
            int randType = Random.Range(0, 100);
            if (randType < 10)
                type = 3;
            else if (randType < 40)
                type = 2;
            else
                type = 1;

            int randModule = Random.Range(0, modulesLvl1.Length);
           // 
            GameObject Go = transform.GetChild(i).gameObject;
            if(type == 1)
                Instantiate(modulesLvl1[randModule], Go.transform, false);
            else if(type == 2)
                Instantiate(modulesLvl2[randModule], Go.transform, false);
            else if (type == 3)
                Instantiate(modulesLvl3[randModule], Go.transform, false);

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