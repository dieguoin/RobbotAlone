using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftArm : BodyParts
{
    //[SerializeField] private GameObject bulletPrefab;
    public GameObject AudioEffects;
    public AudioClip gunShot;
    public AudioClip meleeShot;


    [SerializeField] const int SHOOTFORCE = 400;

    public override void Effect(Animator animator)
    {
        Debug.Log("LeftArm");
        switch (part.weapon)
        {
            case (InGameObjects.ArmType.Gun):
                GameObject bullet = GameObject.Instantiate(part.bulletPrefab, transform.position, transform.rotation);
                animator.SetBool("LeftShot", true);
                bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.Normalize(Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position) * SHOOTFORCE, 0);
                AudioEffects.GetComponent<AudioSource>().clip = gunShot;
                AudioEffects.GetComponent<AudioSource>().Play();
                break;
            case (InGameObjects.ArmType.Sword):
                AudioEffects.GetComponent<AudioSource>().clip = meleeShot;
                AudioEffects.GetComponent<AudioSource>().Play();

                animator.SetBool("LeftHit", true);
                //animator.SetBool("RightHit", false);
                //Actuar con brazo de espada (Animación)
                break;
        }
    }
}
