using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    
    const float SPEED = 5;
    private int moveingKeys = 0;
    private float speed = 0;
    const float DEFAULTMULTIPLAYER = 1;
    const float MULTIPLAYER = 1.3f;
    private float speedMultiplayer = 1;
    

    private Inventory inventory;
    
    //Collision
    [Header("collision")]
    public float jumpForce;
    public Vector3 squareSize;
    public float maxDistance;
    public LayerMask layerMask;
    private Rigidbody2D rb;

    //shoot
    [Header("shoot")]
    [SerializeField] private GameObject bulletPrefab;
    private int bulletDirection = 1;
    public int shootForce;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        CheckJump();
        CheckLeftAction();
        CheckRightAction();
        transform.position = transform.position + new Vector3(speed * speedMultiplayer * Time.deltaTime, 0, 0);
    }
    private void CheckMovement()
    {
        if (Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("Right")))
        {
            ChangeSpeed(SPEED);
            moveingKeys = (moveingKeys < 2) ? moveingKeys + 1: 2;
        }
        if (Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("Left")))
        {
            ChangeSpeed(-SPEED);
            moveingKeys = (moveingKeys < 2) ? moveingKeys + 1: 2;
        }
        speedMultiplayer = DEFAULTMULTIPLAYER;
        if (Input.GetKey(gameManager.GetComponent<GameManager>().GetAction("Run")))
        {
            speedMultiplayer = MULTIPLAYER;
        }
        if (Input.GetKeyUp(gameManager.GetComponent<GameManager>().GetAction("Right")))
        {
            moveingKeys = (moveingKeys > 0) ? moveingKeys - 1: 0;
            if(moveingKeys > 0)
                ChangeSpeed(-SPEED);
        }
        if (Input.GetKeyUp(gameManager.GetComponent<GameManager>().GetAction("Left")))
        {
            moveingKeys = (moveingKeys > 0) ? moveingKeys - 1: 0;
            if(moveingKeys > 0)
                ChangeSpeed(SPEED);
        }
        if(IsGrounded() && moveingKeys != 0)
        {
            ChangeSpeed((speed > 0) ? SPEED : -SPEED);
        }
        if (moveingKeys == 0)
        {
            ChangeSpeed(0);
        }
    }
    private void ChangeSpeed(float newSpeed)
    {
        if (IsGrounded())
        {
            speed = newSpeed;
        }
        else
        {
            speed = (newSpeed < 0) ? -1 : (newSpeed > 0) ? 1 : 0;
        }
        if(speed > 0)
        {
            bulletDirection = 1;
        }
        else if(speed < 0)
        {
            bulletDirection = -1;
        }
        //bulletDirection = (speed > 0) ? 1 : ((speed < 0) ? -1 : bulletDirection);
    }
    private bool IsGrounded()
    {
        if(Physics2D.BoxCast(transform.position, squareSize, 0, -transform.up, maxDistance, layerMask))
        {
            return true;
        }
        return false;
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawCube(transform.position - transform.up * maxDistance, squareSize);
    }
    private void CheckJump()
    {
        
        if (Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("Jump")) && IsGrounded())
        {
            rb.AddForce(new Vector2(0, jumpForce * speedMultiplayer));
        }
    }
    private void CheckLeftAction()
    {
        if (!Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("LeftAction")))
        {
            return;
        }
        Debug.Log("Left");
        //if (Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("Shoot")))
        //{
        //    GameObject bullet = GameObject.Instantiate(bulletPrefab, transform.position, transform.rotation);
        //    bullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(bulletDirection * shootForce, 0));
        //}
    }
    private void CheckRightAction()
    {
        if (!Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("RightAction")))
        {
            return;
        }
        Debug.Log("right");
    }
}
