using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    // Start is called before the first frame update
    [Header("General")]

    public bool opened;

    [Header("DmgDoor")]
    public bool doesDmg;
    public float timeDmg;
    public int dmg;
    public bool dmgCD;

    [Header("BendDownDoor")]
    public bool bendDoor;
    public float timeActive;

    public PlayerMovement player;



    void Start()
    {
        opened = false;
        dmgCD = true;

        player = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            if (!opened && doesDmg && dmgCD)
            {
                Debug.Log("DO DMG " + dmg);
                player.ChangeLife(dmg);

                ///IMPLEMENTAR BAJAR VIDA AL JUGADOR
                StartCoroutine(CoolingDown());

            }


        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (bendDoor)
        {
            // if(collision.gameObject.TryGetComponent<PlayerMovement>().bendedDown == true)
            if (collision.gameObject.TryGetComponent(out PlayerMovement player))
            {
                if (player.bendedDown)
                {
                    this.GetComponent<Collider2D>().enabled = false;
                    StartCoroutine(ActivateDoor());
                }
            }
        }
    }

 

    public IEnumerator CoolingDown()
    {
        dmgCD = false;

        yield return new WaitForSeconds(timeDmg);

        dmgCD = true;

    }

    public IEnumerator ActivateDoor()
    {
        yield return new WaitForSeconds(timeActive);
        Debug.Log(this.GetComponent<Collider2D>().enabled);

        this.GetComponent<Collider2D>().enabled = true;

        Debug.Log(this.GetComponent<Collider2D>().enabled);


    }
}
