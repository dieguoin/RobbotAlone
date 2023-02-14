using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    private float timer = 0;
    private float maxTime = 5;
    public int dmg;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= maxTime)
        {
            Debug.Log("DESTROY BULLET");
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Enemy" && collision.tag != "Ignore" && collision.tag !="BulletEnemy")
        {
            if (collision.tag != "Player" && collision.tag != "RegionSpawner")
            {
                Debug.Log(collision.tag);
                Debug.Log("DESTROY BULLET");
                Destroy(gameObject);
            }
        }
        if(collision.tag == "Player"){
            collision.GetComponent<PlayerMovement>().ChangeLife(dmg);
            Destroy(gameObject);
        }
    }

}
