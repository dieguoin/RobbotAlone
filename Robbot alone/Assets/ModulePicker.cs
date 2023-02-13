using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePicker : MonoBehaviour
{
    // Start is called before the first frame update
    public int type;
    public InGameObjects ingame;

    public PlayerMovement player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
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
            //inventory.modulesBackPacK.Add(ingame);
            inventory.objectsBackPack.Add(ingame);
            player.AddModule(ingame.name, type, ingame.sprite);
            //Añadir variables a PlayerMovement;
            /*
            if(ingame.name == "Jetpack")
            {
                player.secondJump = true;
                if (type == 2)
                    player.secondJumpMultiplier = 1.5f;
                else if (type == 3)
                    player.secondJumpMultiplier = 2.2f;
                else
                    player.secondJumpMultiplier = 1.0f;
            }
            else if(ingame.name == "Shield")
            {
                player.shield = true;
                if (type == 2)
                    player.shieldCD = 40;
                else if (type == 3)
                    player.shieldCD = 30;
                else
                    player.shieldCD = 50;
            }*/
            Destroy(this.gameObject);

        }
    }
}
