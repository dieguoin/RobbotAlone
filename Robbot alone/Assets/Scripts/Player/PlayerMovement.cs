using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();

    const float SPEED = 5;
    private int moveingKeys = 0;
    private float speed = 0;
    const float DEFAULTMULTIPLAYER = 1;
    const float MULTIPLAYER = 1.3f;
    private float speedMultiplayer = 1;

    //Collision
    public float jumpForce;
    public Vector3 squareSize;
    public float maxDistance;
    public LayerMask layerMask;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        keys.Add("up", KeyCode.W);
        keys.Add("Down", KeyCode.S);
        keys.Add("Left", KeyCode.A);
        keys.Add("Right", KeyCode.D);
        keys.Add("Run", KeyCode.LeftControl);
        keys.Add("Pause", KeyCode.Escape);
        keys.Add("Jump", KeyCode.Space);
        keys.Add("Shoot", KeyCode.Insert);
    }

    // Update is called once per frame
    void Update()
    {
        CheckMovement();
        CheckJump();
        transform.position = transform.position + new Vector3(speed * speedMultiplayer * Time.deltaTime, 0, 0);
    }
    private void CheckMovement()
    {
        if (Input.GetKeyDown(keys["Right"]))
        {
            if (IsGrounded())
            {
                speed = SPEED;
            }
            else
            {
                speed = 1;
            }
            moveingKeys++;
        }
        if (Input.GetKeyDown(keys["Left"]))
        {
            if (IsGrounded())
            {
                speed = -SPEED;
            }
            else
            {
                speed = -1;
            }
            moveingKeys++;
        }
        speedMultiplayer = DEFAULTMULTIPLAYER;
        if (Input.GetKey(keys["Run"]))
        {
            speedMultiplayer = MULTIPLAYER;
        }
        if (Input.GetKeyUp(keys["Right"]))
        {
            moveingKeys--;
            if (IsGrounded())
            {
                speed = -SPEED;
            }
            else
            {
                speed = -1;
            }
        }
        if (Input.GetKeyUp(keys["Left"]))
        {
            moveingKeys--;
            if (IsGrounded())
            {
                speed = SPEED;
            }
            else
            {
                speed = 1;
            }
        }
        if(IsGrounded() && moveingKeys != 0)
        {
            speed = (speed > 0) ? SPEED : -SPEED;
        }
        if (moveingKeys == 0)
        {
            speed = 0;
        }
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
        
        if (Input.GetKeyDown(keys["Jump"]) && IsGrounded())
        {
            Debug.Log("jump");
            rb.AddForce(new Vector2(0, jumpForce * speedMultiplayer));
        }
    }
}
