using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModulePicker : MonoBehaviour
{
    // Start is called before the first frame update
  //  public int type;
    public InGameObjects ingame;

    public PlayerMovement player;
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
      /*  int randType = Random.Range(0, 100);
        if (randType < 10)
            type = 3;
        else if (randType < 40)
            type = 2;
        else
            type = 1;*/
      if(gameObject.GetComponent<SpriteRenderer>().sprite == null)
        {
            Debug.Log("NULL SPRITE");
            gameObject.GetComponent<SpriteRenderer>().sprite = ingame.sprite;
        }


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
            int index = inventory.objectsBackPack.IndexOf(ingame) % 3;
          //  inventory.lvls[index] = type;
            Destroy(this.gameObject);

        }
    }
}
