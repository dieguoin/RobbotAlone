using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseControl : MonoBehaviour
{
    private GameObject objectToClone = null;
    private GameObject originalGrabbed = null;
    private GameObject clone = null;
    private GameObject positionToDrop = null;
    private bool objectCloned = false;
    private BackPackManager backPackManager;
    [SerializeField] private ScrollRect menuMovement;

    private void Awake()
    {
        backPackManager = GameObject.Find("BackPack").GetComponent<BackPackManager>();
    }

    private void Update()
    {
        transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }
    private void OnMouseDown()
    {
        if(objectToClone == null)
        {
            return;
        }
        originalGrabbed = objectToClone;
        menuMovement.enabled = false;
        clone = GameObject.Instantiate(objectToClone, transform);
        clone.transform.localPosition = new Vector3(0, 0);
        clone.transform.localScale = new Vector3(.3f, .3f);
        clone.GetComponent<SpriteRenderer>().size = new Vector2(1, 1);

        
        if(originalGrabbed.transform.parent != null)
        {
            if(originalGrabbed.transform.parent.tag == "InventoryHead" || originalGrabbed.transform.parent.tag == "InventoryLeftArm" || originalGrabbed.transform.parent.tag == "InventoryRightArm" || originalGrabbed.transform.parent.tag == "InventoryBody" || originalGrabbed.transform.parent.tag == "InventoryLegs")
            {

                originalGrabbed.transform.parent.GetComponent<Image>().enabled = true;
                originalGrabbed.transform.parent.GetComponent<BoxCollider2D>().enabled = true;
                Destroy(originalGrabbed);
                Debug.Log(clone.transform.localPosition);
                objectToClone = null;
                originalGrabbed = null;
            }
            else if (originalGrabbed.transform.parent.tag == "BackPackMan")
            {
                backPackManager.RemoveObject(originalGrabbed);
            }
        }
        
        objectCloned = true;
    }
    private void OnMouseUp()
    {
        if(!objectCloned) { return; }
        menuMovement.enabled = true;
        if(originalGrabbed == null)
        {
            //inventory.AddObject(clone);
        }
        if(positionToDrop == null || positionToDrop.transform.childCount > 0)
        {
            clone.GetComponent<SpriteRenderer>().size = new Vector2(30, 30);
            clone.transform.localScale = new Vector3(1, 1);
            backPackManager.AddObject(clone);
            Destroy(clone);
            objectCloned = false;
            originalGrabbed = null;
            return;
        }
        GameObject newObject = GameObject.Instantiate(clone, positionToDrop.transform);
        positionToDrop.GetComponent<Image>().enabled = false;
        positionToDrop.GetComponent<BoxCollider2D>().enabled = false;
        newObject.GetComponent<SpriteRenderer>().size = new Vector2(30, 30); 
        newObject.transform.localScale = new Vector3(1, 1);
        newObject.transform.localPosition = new Vector3(0, 0);
        Destroy(clone);
        clone = null;
        objectCloned = false;
        originalGrabbed = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!objectCloned && collision.tag == "ObjectToMove")
        {
            objectToClone = collision.gameObject;
        }
        else
        {
            if(collision.tag == "InventoryHead" && clone.GetComponent<ObjectInteraction>().objectType.type == InGameObjects.Type.Head)
            {
                positionToDrop = collision.gameObject;
            }
            else
            if (collision.tag == "InventoryLeftArm" && clone.GetComponent<ObjectInteraction>().objectType.type == InGameObjects.Type.LeftArm)
            {
                positionToDrop = collision.gameObject;
            }
            else
            if (collision.tag == "InventoryRightArm" && clone.GetComponent<ObjectInteraction>().objectType.type == InGameObjects.Type.RightArm)
            {
                positionToDrop = collision.gameObject;
            }
            else
            if (collision.tag == "InventoryBody" && clone.GetComponent<ObjectInteraction>().objectType.type == InGameObjects.Type.Body)
            {
                positionToDrop = collision.gameObject;
            }
            else
            if (collision.tag == "InventoryLegs" && clone.GetComponent<ObjectInteraction>().objectType.type == InGameObjects.Type.Leg)
            {
                positionToDrop = collision.gameObject;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        objectToClone = null;
        positionToDrop = null;
    }
}
