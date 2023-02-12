using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackPackManager : MonoBehaviour
{
    public GameObject defaultObject;
    public List<GameObject> backPackList;
    private Inventory inventory;
    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    private void Start()
    {
        foreach(GameObject g in inventory.objectsBackPack)
        {
            GameObject newG = GameObject.Instantiate(defaultObject, transform.GetChild(0).GetChild(0).GetChild(0));
            newG.GetComponent<Image>().sprite = g.GetComponent<SpriteRenderer>().sprite;
            newG.GetComponent<SpriteRenderer>().sprite = g.GetComponent<SpriteRenderer>().sprite;
            newG.GetComponent<SpriteRenderer>().size = new Vector2(30, 30);
            Component c = newG.AddComponent(typeof(ObjectInteraction));
            c.GetComponent<ObjectInteraction>().objectType = g.GetComponent<ObjectInteraction>().objectType;
            backPackList.Add(newG);
            //newG.GetComponent<Image>().sprite = g.GetComponent<SpriteRenderer>().sprite;
        }
    }
    public void AddObject(GameObject newObject)
    {
        RemoveEverything();
        backPackList.Add(newObject);
        UpdateBackPack();
    }
    public void RemoveObject(GameObject objectToRemove)
    {
        RemoveEverything();
        backPackList.Remove(objectToRemove);
        UpdateBackPack();
    }
    private void RemoveEverything()
    {
        for (int i = 0; i < backPackList.Count; i++)
        {
            Destroy(backPackList[i]);
        }
    }
    private void UpdateBackPack()
    {
        for(int i = 0; i < backPackList.Count; i++)
        {
            backPackList[i] = GameObject.Instantiate(backPackList[i], transform.GetChild(0).GetChild(0).GetChild(0));
            backPackList[i].GetComponent<Image>().enabled = true;
            backPackList[i].GetComponent<BoxCollider2D>().enabled = true;
        }
    }
}
