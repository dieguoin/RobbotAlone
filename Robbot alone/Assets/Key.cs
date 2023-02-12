using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject doorObj;

    Door door;
    void Start()
    {
       door = doorObj.GetComponent<Door>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            Debug.Log("ENTER COLL");
            door.opened = true;
            doorObj.GetComponent<BoxCollider2D>().enabled = false;
        }


    }
}
