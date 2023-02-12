using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultObject;
    private Inventory inventory;
    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void Start()
    {
        int i = 0;
        foreach (InGameObjects g in inventory.bodyParts)
        {
            if(g != null)
            {
                GameObject newG = GameObject.Instantiate(defaultObject, transform.GetChild(1).GetChild(i));
                transform.GetChild(1).GetChild(i).GetComponent<Image>().enabled = false;
                transform.GetChild(1).GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                newG.GetComponent<Image>().sprite = g.sprite;
                newG.GetComponent<SpriteRenderer>().sprite = g.sprite;
                newG.GetComponent<SpriteRenderer>().size = new Vector2(30, 30);
                newG.transform.localPosition = new Vector3(0, 0);
                newG.AddComponent(typeof(ObjectInteraction));
                newG.GetComponent<ObjectInteraction>().objectType = g;
                //newG.GetComponent<Image>().sprite = g.GetComponent<SpriteRenderer>().sprite;
            }
            i++;
        }
    }
    public InGameObjects GetBodyPart(int i)
    {
        Debug.Log(transform.GetChild(1).GetChild(i));
        return (transform.GetChild(1).GetChild(i).childCount > 0) ? transform.GetChild(1).GetChild(i).GetChild(0).gameObject.GetComponent<ObjectInteraction>().objectType : null;
    }
}
