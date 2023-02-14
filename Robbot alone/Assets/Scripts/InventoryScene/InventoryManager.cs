using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultObject;
    private Inventory inventory;

    public Sprite DefHead;
    public Sprite DefRighArm;
    public Sprite DefLeftArm;
    public Sprite DefBody;
    public Sprite DefLegs;


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
                GameObject newG = null;
                if (i < 5)
                {
                    newG = GameObject.Instantiate(defaultObject, transform.GetChild(1).GetChild(i));
                    transform.GetChild(1).GetChild(i).GetComponent<Image>().enabled = false;
                    transform.GetChild(1).GetChild(i).GetComponent<BoxCollider2D>().enabled = false;
                }
                else
                {
                    int j = i - 5;
                    newG = GameObject.Instantiate(defaultObject, transform.GetChild(2).GetChild(j));
                    transform.GetChild(2).GetChild(j).GetComponent<Image>().enabled = false;
                    transform.GetChild(2).GetChild(j).GetComponent<BoxCollider2D>().enabled = false;
                }
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
        if(i < 5)
        {
            Debug.Log(transform.GetChild(1).GetChild(i));
            return (transform.GetChild(1).GetChild(i).childCount > 0) ? transform.GetChild(1).GetChild(i).GetChild(0).gameObject.GetComponent<ObjectInteraction>().objectType : null;
        }
        else
        {
            Debug.Log(i);
            int j = i - 5;
            //Debug.Log(transform.GetChild(2).name);
            Debug.Log(j);
            Debug.Log(transform.childCount);
            Debug.Log(transform.GetChild(2).GetChild(j));

            return (transform.GetChild(2).GetChild(j).childCount > 0) ? transform.GetChild(2).GetChild(j).GetChild(0).gameObject.GetComponent<ObjectInteraction>().objectType : null;
        }

        
    }
}
