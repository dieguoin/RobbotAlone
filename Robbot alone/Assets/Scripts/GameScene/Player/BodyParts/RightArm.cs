using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArm : BodyParts
{
    //[SerializeField] private GameObject bulletPrefab;
    [SerializeField] const int SHOOTFORCE = 200;
    public override void Effect()
    {
        switch (part.weapon)
        {
            case (InGameObjects.ArmType.Gun):
                GameObject bullet = GameObject.Instantiate(part.bulletPrefab, transform.position, transform.rotation);
                Debug.Log(Input.mousePosition - transform.position);
                bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * SHOOTFORCE, 0);
                break;
            case (InGameObjects.ArmType.Sword):
                //Actuar con brazo de espada (Animación)
                break;
        }
    }
}
