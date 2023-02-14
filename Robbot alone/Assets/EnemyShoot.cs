using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    // Start is called before the first frame update

    public Enemy shooterEn;

    public bool readyShot;

    public GameObject bulletPrefab;

    public float SHOOTFORCE;
    void Start()
    {
        readyShot = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player" && readyShot)
        {
            readyShot = false;
            shooterEn.currentState = Enemy.States.Attacking;

            GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce((collision.transform.position - transform.position) * SHOOTFORCE, 0);
            

            Debug.Log("disparando");
            StartCoroutine(Reload());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            shooterEn.currentState = Enemy.States.Patrol;

        }
    }


    IEnumerator Shot()
    {
        yield return new WaitForSeconds(1.5f);
        shooterEn.currentState = Enemy.States.Patrol;
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1);
        //shooterEn.currentState = Enemy.States.Idle;
        // shooterEn.currentState = Enemy.States.Patrol;
        readyShot = true;
    }
}
