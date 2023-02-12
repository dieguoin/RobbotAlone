using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingChecker : MonoBehaviour
{
    // Start is called before the first frame update

    public Door parentDoor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTRA TRIGGER");
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            Debug.Log("COGE PLAYER ");
            Debug.Log(player.isJumping);
            if (player.isJumping)
            {
                Debug.Log("PLAYER IS JUMPING");
                parentDoor.GetComponent<Collider2D>().enabled = false;
                StartCoroutine(parentDoor.ActivateDoor());
            }
        }
    }
}
