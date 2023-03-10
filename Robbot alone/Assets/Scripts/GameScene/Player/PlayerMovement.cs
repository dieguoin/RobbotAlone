using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.U2D.Animation;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject gameManager;
    
    const float SPEED = 5;
    private int moveingKeys = 0;
    private float speed = 0;
    const float DEFAULTMULTIPLAYER = 1;
    const float MULTIPLAYER = 1.1f;
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

    [Header("Life")]
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

    public List<SpriteResolver> headSprites;
    public List<SpriteResolver> bodySprites;
    public List<SpriteResolver> rightArmSprites;
    public List<SpriteResolver> leftArmSprites;
    public List<SpriteResolver> legsSprites;

    [SerializeField] private InGameObjects defaultHead;
    [SerializeField] private InGameObjects defaultBody;
    [SerializeField] private InGameObjects defaultLeftArm;
    [SerializeField] private InGameObjects defaultRightArm;
    [SerializeField] private InGameObjects defaultLegs;


    [Header("Jetpack")]
    public float secondJumpMultiplier;
    public bool jumped;

    [Header("AvailableModules")]
    public bool secondJump;
    public bool shield;

    public int shieldCD;

    //Dictionary<string, Module> modules = new Dictionary<string, Module>();
    // public List<Module> modules = new List<Module>();
    [SerializeField]
    public Module[] modules;
    public int index;

    public Image[] uiModules;
    public GameObject uiModParent;

    //Animations
    [Header("Animations")]
    private AnimatorController animatorController;


    public Animator shieldAnim;
   // public SpriteRenderer shieldImg;


    public GameObject[] playerSpawner;

    public AudioClip hit;

    public GameObject AudioEffects;


    // Start is called before the first frame update
    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        animatorController = GetComponentInChildren<AnimatorController>();
    }
    void Start()
    {
        head.part = (inventory.bodyParts[0] != null) ? inventory.bodyParts[0] : defaultHead;
        leftArm.part = (inventory.bodyParts[1] != null) ? inventory.bodyParts[1] : defaultLeftArm;
        rightArm.part = (inventory.bodyParts[2] != null) ? inventory.bodyParts[2] : defaultRightArm;
        body.part = (inventory.bodyParts[3] != null) ? inventory.bodyParts[3] : defaultBody;
        legs.part = (inventory.bodyParts[4] != null) ? inventory.bodyParts[4] : defaultLegs;

        //colocar sprites
        ChangeSprite(headSprites, head.part);
        ChangeSprite(bodySprites, body.part);
        ChangeSprite(leftArmSprites, leftArm.part);
        ChangeSprite(rightArmSprites, rightArm.part);
        ChangeSprite(legsSprites, legs.part);

        //Calcular Vida M?xima
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



        rb = GetComponent<Rigidbody2D>();
        bendedDown = false;
        isJumping = false;

      
        jumped = false;

        modules = new Module[3];
        uiModules = new Image[3];

        for (int i = 0; i < uiModParent.transform.childCount; i++)
        {
            uiModules[i] = uiModParent.transform.GetChild(i).gameObject.GetComponent<Image>();
            uiModParent.transform.GetChild(i).gameObject.GetComponent<Image>().enabled = false;
            uiModules[i].enabled = false;
        }


        for(int i = 0; i< inventory.bodyParts.Count; i++)
        {
            if (inventory.bodyParts[i] != null)
            {
                if (inventory.bodyParts[i].type == InGameObjects.Type.Module)
                {
                       AddModule(inventory.bodyParts[i].name, 0, inventory.bodyParts[i].sprite);
                }
            }
        }
        life = Stats.lifePoints;
        lifeUI = 1;
        lifeImg.fillAmount = 1;

        int randSpawn = Random.Range(0, playerSpawner.Length);
        transform.position = playerSpawner[randSpawn].transform.position;
        playerSpawner[randSpawn].transform.parent.gameObject.SetActive(true);

        if (shield)
        {
           // shieldImg.enabled = true;
            shieldAnim.SetInteger("ShieldState", 1);
        }
        else
        {
            shieldAnim.SetInteger("ShieldState", 0);
         //   shieldImg.enabled = false;

        }



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
        animatorController.Move(speed);
        
        //bulletDirection = (speed > 0) ? 1 : ((speed < 0) ? -1 : bulletDirection);
    }
    private bool IsGrounded()
    {
        if(Physics2D.BoxCast(transform.position, squareSize, 0, -transform.up, maxDistance, layerMask))
        {
            isJumping = false;
            //jumped = true;
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
        if(Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("Jump"))) {
                if (secondJump && !jumped && isJumping)
                {
                    jumped = true;
                    rb.AddForce(new Vector2(0, jumpForce * speedMultiplayer * secondJumpMultiplier)); //multiplicar por jumpMultiplier
                }
            }
        if (Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("Jump")) && IsGrounded())
        {
            animatorController.EnableJump(true);
            //WaitTime(1);
            //Jump();
        }
        //animator.SetBool("Jump", !IsGrounded());
        animatorController.SetGrounded(IsGrounded());
    }
    public void Jump()
    {
        rb.AddForce(new Vector2(0, jumpForce * speedMultiplayer));
        jumped = false;
        // isJumping = true;
    }
    private void CheckLeftAction()
    {
        if (!Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("LeftAction")))
        {
            return;
        }
        leftArm.Effect(animatorController.animator);
    }
    private void CheckRightAction()
    {
        if (!Input.GetKeyDown(gameManager.GetComponent<GameManager>().GetAction("RightAction")))
        {
            return;
        }
        rightArm.Effect(animatorController.animator);
    }

    public void ChangeLife(int dmgReceived)
    {
        if (shield)
        {
            StartCoroutine(ShieldCooling());
            return;
        }
        int dmge = dmgReceived - Stats.defense ;
        if (dmge < 1)
            dmge = 1;
        life -= dmge;
        lifeUI = (float)life / (float)Stats.lifePoints;

        AudioEffects.GetComponent<AudioSource>().clip = hit;
        AudioEffects.GetComponent<AudioSource>().Play();

        lifeImg.fillAmount = lifeUI;
        if(life <= 0)
        {
            gameManager.GetComponent<GameManager>().Death();
        }
    }
    private IEnumerator WaitTime(int time)
    {
        yield return new WaitForSeconds(time);
    }
    IEnumerator ShieldCooling()
    {
        shieldAnim.SetInteger("ShieldState", 2);
        StartCoroutine(ReturnTime());
        shield = false;
        yield return new WaitForSeconds(shieldCD);
       // shieldImg.enabled = true;
        shieldAnim.SetInteger("ShieldState", 1);
        shield = true;
    }
    IEnumerator ReturnTime()
    {
        yield return new WaitForSeconds(2);
        shieldAnim.SetInteger("ShieldState", 0);
     //   shieldImg.enabled = false;

    }

    public void AddModule(string mod, int tipo, Sprite sprite)
    {
        Module m = new Module(mod, tipo, sprite);
        if(index == 2)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        modules[index] = m;
        uiModules[index].sprite = sprite;
        uiModParent.transform.GetChild(index).gameObject.GetComponent<Image>().sprite = sprite;
        uiModParent.transform.GetChild(index).gameObject.GetComponent<Image>().enabled = true;


        if (mod == "Jetpack")
        {
            secondJump = true;
            if (tipo == 2)
                secondJumpMultiplier = 1.5f;
            else if (tipo == 3)
                secondJumpMultiplier = 2.2f;
            else
                secondJumpMultiplier = 1.0f;
        }
        else if (mod == "Shield")
        {
            shield = true;
            if (tipo == 2)
                shieldCD = 40;
            else if (tipo == 3)
                shieldCD = 30;
            else
                shieldCD = 50;
        }

        if (mod == "Attack")
        {
            if (tipo == 2)
                Stats.attack += 3;
            else if (tipo == 3)
                Stats.attack += 4;
            else
                Stats.attack += 5;
        }

        if(mod == "Speed")
        {
            if (tipo == 2)
                Stats.speed += 3;
            else if (tipo == 3)
                Stats.speed += 4;
            else
                Stats.speed += 5;
        }
        if (mod == "Defense")
        {
            if (tipo == 2)
                Stats.defense += 3;
            else if (tipo == 3)
                Stats.defense += 4;
            else
                Stats.defense += 5;
        }
        if (mod == "Life")
        {
            if (tipo == 2)
                Stats.lifePoints += 3;
            else if (tipo == 3)
                Stats.lifePoints += 4;
            else
                Stats.lifePoints += 5;
        }

    }
    private void ChangeSprite(List<SpriteResolver> gameObjects, InGameObjects iObject)
    {
        foreach (SpriteResolver rs in gameObjects)
        {
            rs.SetCategoryAndLabel(rs.GetCategory(), iObject.spriteName);
        }
    }
}
