using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleLoot : MonoBehaviour
{
    // Start is called before the first frame update

    // Start is called before the first frame update

    //public Module [] modules;
    public GameObject[] modulesLvl1;
    public GameObject[] modulesLvl2;
    public GameObject[] modulesLvl3;

    public GameObject[] parts;


    void Start()
    {
        
            int type = 0;
            int randType = Random.Range(0, 100);
        if (randType < 35)
            return;
        else if (randType < 55)
            type = 3;
        else if (randType < 65)
            type = 2;
        else if (randType < 70)
            type = 1;
        else
            type = 4;

            int randModule = Random.Range(0, modulesLvl1.Length);
            // 
            GameObject Go = transform.gameObject;
            if (type == 1)
                Instantiate(modulesLvl1[randModule], Go.transform, false);
            else if (type == 2)
                Instantiate(modulesLvl2[randModule], Go.transform, false);
            else if (type == 3)
                Instantiate(modulesLvl3[randModule], Go.transform, false);
            else
                Instantiate(parts[Random.Range(0, parts.Length)], Go.transform, false);
    }
}
