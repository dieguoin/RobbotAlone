using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Extractor : MonoBehaviour
{
    // Start is called before the first frame update
    public SceneMan sceneManager;

    void Start()
    {
        sceneManager = GameObject.Find("SceneMan").GetComponent<SceneMan>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("COL");

        if (collision.name == "Player"){
            Debug.Log("CHANGEEEE");
            StartCoroutine(waitToChange());
        }
    }

    IEnumerator waitToChange()
    {

        yield return new WaitForSeconds(1);
        Debug.Log("CHANGING");
        sceneManager.changeScene("InventoryMenu");
    }
}
