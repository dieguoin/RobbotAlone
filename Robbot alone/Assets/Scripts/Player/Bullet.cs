using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float timer = 0;
    private float maxTime = 5;
    private void Update()
    {
        timer += Time.deltaTime;
        if(timer >= maxTime)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != "Player" && collision.tag != "RegionSpawner")
        {
            Destroy(gameObject);
        }
    }
}
