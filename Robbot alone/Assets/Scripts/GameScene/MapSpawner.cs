using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    public new Collider2D collider;

    public GameObject mapRegion;

    public GameObject unLoadRegion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "Player")
        {
            mapRegion.SetActive(true);

            if(unLoadRegion != null)
            {
                unLoadRegion.SetActive(false);
            }
        }

        
    }
}
