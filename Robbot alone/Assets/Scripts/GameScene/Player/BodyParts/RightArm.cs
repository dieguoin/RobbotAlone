using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArm : BodyParts
{
    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] const int SHOOTFORCE = 2;
    protected override void Effect()
    {
        switch (part.weapon)
        {
            case (InGameObjects.ArmType.Gun):
                GameObject bullet = GameObject.Instantiate(part.bulletPrefab, transform.position, transform.rotation);
                bullet.GetComponent<Rigidbody2D>().AddForce((Input.mousePosition - transform.position) * SHOOTFORCE, 0);
                break;
            case (InGameObjects.ArmType.Sword):
                //Actuar con brazo de espada (Animación)
                break;
        }
    }
}
