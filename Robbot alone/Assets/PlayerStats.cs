using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats VarInstance;

    public float life;
    public float defense;
    public float speedMultplier;

void Awake()
    {
        DontDestroyOnLoad(this);

        if (VarInstance == null)
        {
            VarInstance = this;
        }
        else
        {
            DestroyObject(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
