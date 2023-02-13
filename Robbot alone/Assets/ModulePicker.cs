using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePicker : MonoBehaviour
{
    // Start is called before the first frame update
    public int type;
    public InGameObjects ingame;
    void Start()
    {
        int randType = Random.Range(0, 100);
        if (randType < 10)
            type = 3;
        else if (randType < 40)
            type = 2;
        else
            type = 1;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            Inventory inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
            inventory.objectsInventory.Add(ingame);
            //Añadir variables a PlayerMovement;
            Destroy(this.gameObject);

        }
    }
}
