using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int nextPosition = 0;
    public enum States {Patrol, Attacking, Idle};
    public States currentState;

    [SerializeField] public GameObject pointPrefab;
    [SerializeField] private List<Transform> patrolPositions = new List<Transform>();
    public float speed;
    private Rigidbody2D rb;

    public bool dmgCD;
    public int dmg;
    public float timeDmg;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        dmgCD = true;
        if(currentState == States.Patrol)
        {
            transform.GetChild(1).parent = transform.parent;
        }
    }

    private void Update()
    {
        switch (currentState)
        {

            case States.Patrol:
                if(patrolPositions.Count == 0)
                {
                    return;
                }
                if(patrolPositions[nextPosition].position.x - transform.position.x < 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                transform.position = Vector3.MoveTowards(transform.position, patrolPositions[nextPosition].position, speed * Time.deltaTime);
                if(transform.position == patrolPositions[nextPosition].position)
                {
                    nextPosition = (nextPosition + 1) % patrolPositions.Count;
                }
                break;
            case States.Attacking:
                Debug.Log("SHOOOT");
                currentState = States.Idle;
                break;
            case States.Idle:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            Debug.Log("DESTROY");
            Destroy(gameObject);
        }
        if (collision.tag == "Sword")
        {
            Debug.Log("DESTROY");
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (dmgCD)
            {
                Debug.Log("DO DMG " + dmg);
                collision.gameObject.GetComponent<PlayerMovement>().ChangeLife(dmg);
                ///IMPLEMENTAR BAJAR VIDA AL JUGADOR
                StartCoroutine(CoolingDown());

            }


        }
    }
    public void GeneratePoint()
    {
        GameObject newPoint = GameObject.Instantiate(pointPrefab, transform.GetChild(1));
        patrolPositions.Add(null);
    }

    public IEnumerator CoolingDown()
    {
        dmgCD = false;

        yield return new WaitForSeconds(timeDmg);

        dmgCD = true;

    }
}
