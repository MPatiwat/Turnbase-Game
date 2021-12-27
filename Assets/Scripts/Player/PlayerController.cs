using UnityEngine;


public class PlayerController : MonoBehaviour
{
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


