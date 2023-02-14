using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject defaultObject;
    private Inventory inventory;

    public List<InGameObjects> defaultSprites;
    public List<SpriteResolver> head;
    public List<SpriteResolver> body;
    public List<SpriteResolver> leftArm;
    public List<SpriteResolver> rightArm;
    public List<SpriteResolver> legs;
    public List<List<SpriteResolver>> bodyParts = new List<List<SpriteResolver>>();


    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void Start()
    {
        bodyParts.Add(head);
        bodyParts.Add(leftArm);
        bodyParts.Add(rightArm);
        bodyParts.Add(body);
        bodyParts.Add(legs);
        int i = 0;
        int k = 0;
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
                    //ChangeSprite(bodyParts[i], (GetBodyPart(i) != null) ? GetBodyPart(i) : defaultSprites[i]);
                        //transform.GetChild(1).GetChild(i).GetComponent<>
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
                if (i < 5)
                {
                    ChangeSprite(bodyParts[i], (GetBodyPart(i) != null) ? GetBodyPart(i) : defaultSprites[i]);
                    //transform.GetChild(1).GetChild(i).GetComponent<>
                }
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
    private void ChangeSprite(List<SpriteResolver> gameObjects, InGameObjects iObject)
    {
        foreach (SpriteResolver rs in gameObjects)
        {
            rs.SetCategoryAndLabel(rs.GetCategory(), iObject.spriteName);
        }
    }
    private void ChangeSprite(SpriteResolver gameObjects, InGameObjects iObject)
    {


        gameObjects.SetCategoryAndLabel(gameObjects.GetCategory(), iObject.spriteName);

    }
}
