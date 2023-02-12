using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InGameObjects GetBodyPart(int i)
    {
        Debug.Log(transform.GetChild(1).GetChild(i));
        return (transform.GetChild(1).GetChild(i).childCount > 0) ? transform.GetChild(1).GetChild(i).GetChild(0).gameObject.GetComponent<ObjectInteraction>().objectType : null;
    }
}
