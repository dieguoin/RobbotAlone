using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    
    const float SPEED = 5;
    private int moveingKeys = 0;
    private float speed = 0;
    const float DEFAULTMULTIPLAYER = 1;
    const float MULTIPLAYER = 1.3f;
    private float speedMultiplayer = 1;
    const float bendDownSpeed = 2f;
    

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

    public bool bendedDown;
    public bool isJumping;

    public int life;
    public Image lifeImg;
    public float lifeUI; //Normalized 0-1

    
    public struct Stats
    {
        public static int lifePoints;
        public static int attack;
        public static int defense;
        public static int speed;
    }

    [Header("BodyParts")]
    public BodyParts head;
    public BodyParts body;
    public BodyParts leftArm;
    public BodyParts rightArm;
    public BodyParts legs;

    [SerializeField] private InGameObjects defaultHead;
    [SerializeField] private InGameObjects defaultBody;
    [SerializeField] private InGameObjects defaultLeftArm;
    [SerializeField] private InGameObjects defaultRightArm;
    [SerializeField] private InGameObjects defaultLegs;

    // Start is called before the first frame update
    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }
    void Start()
    {
        head.part = (inventory.bodyParts[0] != null) ? inventory.bodyParts[0] : defaultHead;
        leftArm.part = (inventory.bodyParts[1] != null) ? inventory.bodyParts[1] : defaultLeftArm;
        rightArm.part = (inventory.bodyParts[2] != null) ? inventory.bodyParts[2] : defaultRightArm;
        body.part = (inventory.bodyParts[3] != null) ? inventory.bodyParts[3] : defaultBody;
        legs.part = (inventory.bodyParts[4] != null) ? inventory.bodyParts[4] : defaultLegs;
        //Calcular Vida M�xima
        Stats.lifePoints = head.part.Life;
        Stats.lifePoints += leftArm.part.Life;
        Stats.lifePoints += rightArm.part.Life;
        Stats.lifePoints += body.part.Life;
        Stats.lifePoints += legs.part.Life;

        //Calcular Ataque
        Stats.attack = head.part.Attack;
        Stats.attack += leftArm.part.Attack;
        Stats.attack += rightArm.part.Attack;
        Stats.attack += body.part.Attack;
        Stats.attack += legs.part.Attack;

        //Calcular Defensa
        Stats.defense = head.part.Defense;
        Stats.defense += leftArm.part.Defense;
        Stats.defense += rightArm.part.Defense;
        Stats.defense += body.part.Defense;
        Stats.defense += legs.part.Defense;

        //Calcular velocidad
        Stats.speed = head.part.Speed;
        Stats.speed += leftArm.part.Speed;
        Stats.speed += rightArm.part.Speed;
        Stats.speed += body.part.Speed;
        Stats.speed += legs.part.Speed;



        Debug.Log(Stats.lifePoints);
        rb = GetComponent<Rigidbody2D>();
        bendedDown = false;
        isJumping = false;

        life = Stats.lifePoints;
        lifeUI = 1;
        lifeImg.fillAmount = 1;
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
            if (bendedDown)
                ChangeSpeed(bendDownSpeed);
            else
                ChangeSpeed(SPEED);
            moveingKeys = (moveingKeys < 2) ? moveingKeys + 1: 2;
        }
        if (Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("Left")))
        {
            if (bendedDown)
                ChangeSpeed(-bendDownSpeed);
            else
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
            if(moveingKeys > 0 && !bendedDown)
                ChangeSpeed(-SPEED);
        }
        if (Input.GetKeyUp(gameManager.GetComponent<GameManager>().GetAction("Left")))
        {
            moveingKeys = (moveingKeys > 0) ? moveingKeys - 1: 0;
            if(moveingKeys > 0 && !bendedDown)
                ChangeSpeed(SPEED);
        }
        if (Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("Control")))
        {
            bendedDown = true;
            transform.position = transform.position + new Vector3(0.0f,0.0f,-0.00001f);
            //ChangeSpeed(bendDownSpeed);
        }
        if (Input.GetKeyUp(gameManager.GetComponent<GameManager>().GetAction("Control")))
        {
            bendedDown = false;
        }
            if (IsGrounded() && moveingKeys != 0 && !bendedDown)
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
            isJumping = false;
            return true;
        }
        isJumping = true;
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
           // isJumping = true;
        }
    }
    private void CheckLeftAction()
    {
        if (!Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("LeftAction")))
        {
            return;
        }
        Debug.Log("Left");
        leftArm.Effect();
    }
    private void CheckRightAction()
    {
        if (!Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("RightAction")))
        {
            return;
        }
        Debug.Log("right");
        rightArm.Effect();
    }

    public void ChangeLife(int dmgReceived)
    {
        life -= dmgReceived;
        lifeUI = (float)life / (float)Stats.lifePoints;

        lifeImg.fillAmount = lifeUI;
        if(life == 0)
        {
            //DEAD
        }
    }
}
