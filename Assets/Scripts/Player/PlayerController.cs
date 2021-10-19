using UnityEngine;


public class PlayerController : MonoBehaviour
{
    /*public float moveSpeed ;
    private Rigidbody2D playerRigidbody;
    private Vector3 moveMent;
    private Animator playerAnimator;
    public static PlayerController instance;
    private bool isMoving = true;
    protected Joystick joystick;

    [Header("Button")]
    [SerializeField] private GameObject topButton;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject bottomButton;



    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
        playerRigidbody = GetComponent<Rigidbody2D>();
        joystick = FindObjectOfType<FixedJoystick>();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        moveMent = Vector3.zero;
        moveMent.x = Input.GetAxisRaw("Horizontal");
        moveMent.y = Input.GetAxisRaw("Vertical");


        RemoveDiagonal();
        UpdateAnimator();
        
    }

    private void RemoveDiagonal()
    {
        
        if (moveMent.x != 0)
        {
            moveMent.y = 0;
        }
        if (moveMent.y != 0)
        {
            moveMent.x = 0;
        }
    }
    private void UpdateAnimator()
    {
        if (moveMent != Vector3.zero)
        {
            playerAnimator.SetFloat("moveX", moveMent.x);
            playerAnimator.SetFloat("moveY", moveMent.y);
            playerAnimator.SetBool("isMoving", true);
        }
        else
        {
            playerAnimator.SetBool("isMoving", false);
        }
    }

    private void FixedUpdate()
    {
        playerRigidbody.MovePosition(transform.position + moveMent * moveSpeed * Time.deltaTime);
        //playerRigidbody.velocity = new Vector3(joystick.Horizontal*moveSpeed * Time.deltaTime , joystick.Vertical* moveSpeed * Time.deltaTime);
    }*/
    public Rigidbody2D playerRigid;
    public float moveSpeed;

    public Animator playerAnimator;
    
    private Vector3 moveMent;
    [SerializeField] public bool canMove;

    public static PlayerController instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        DontDestroyOnLoad(gameObject);
    }
    private void FixedUpdate()
    {
        Joystick joystick = FindObjectOfType<FixedJoystick>();
        moveMent = Vector3.zero;
        if (joystick == null)
        {
            moveMent.x = Input.GetAxisRaw("Horizontal");
            moveMent.y = Input.GetAxisRaw("Vertical");
        }
        else
        {
            moveMent.x = joystick.Horizontal;
            moveMent.y = joystick.Vertical;
        }
        
        RemoveDiagonal();

        if (canMove)
        {
            playerRigid.velocity = new Vector2(moveMent.x, moveMent.y) * moveSpeed * Time.deltaTime;

            playerAnimator.SetFloat("moveX", playerRigid.velocity.x);
            playerAnimator.SetFloat("moveY", playerRigid.velocity.y);
        }
        else
        {
            playerRigid.velocity = new Vector2(0, 0);
            playerAnimator.SetFloat("moveX", playerRigid.velocity.x);
            playerAnimator.SetFloat("moveY", playerRigid.velocity.y);
            
        }
        
        if (moveMent.x == 1 || moveMent.x == -1 || moveMent.y == 1 || moveMent.y == -1)
        {
                playerAnimator.SetFloat("lastMoveX", moveMent.x);
                playerAnimator.SetFloat("lastMoveY", moveMent.y);
            
        }
    }
    private void RemoveDiagonal()
    {

        if (moveMent.x != 0)
        {
            moveMent.y = 0;
        }
        if (moveMent.y != 0)
        {
            moveMent.x = 0;
        }
    }

}


