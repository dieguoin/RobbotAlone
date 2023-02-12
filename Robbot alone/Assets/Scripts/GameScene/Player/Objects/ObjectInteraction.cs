using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteraction : MonoBehaviour
{
    public InGameObjects objectType;
    private Inventory inventory;
    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            inventory.AddObject(objectType);
            Destroy(gameObject);
        }
    }
}
