using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int nextPosition = 0;
    private enum States { Patrol, Attacking};
    private States currentState = States.Patrol;

    [SerializeField] public GameObject pointPrefab;
    [SerializeField] private List<Transform> patrolPositions = new List<Transform>();
    public float speed;

    private void Update()
    {
        switch (currentState)
        {
            case States.Patrol:
                if(patrolPositions.Count == 0)
                {
                    return;
                }
                transform.position = Vector3.MoveTowards(transform.position, patrolPositions[nextPosition].position, speed * Time.deltaTime);
                if(transform.position == patrolPositions[nextPosition].position)
                {
                    nextPosition = (nextPosition + 1) % patrolPositions.Count;
                }
                break;
            case States.Attacking:

                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Bullet")
        {
            Destroy(gameObject);
        }
    }
    public void GeneratePoint()
    {
        GameObject newPoint = GameObject.Instantiate(pointPrefab, transform.parent);
        patrolPositions.Add(newPoint.transform);
    }
}
