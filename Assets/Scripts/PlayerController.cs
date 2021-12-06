using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] int MAX_HORIZONTAL_SPEED = 5;
    [SerializeField] LayerMask platformLayer;
    [SerializeField] LayerMask movingLayer;
    [SerializeField] GameObject groundCheck;
    //for testing purposes only
    public bool isGrounded = false; //to see if player is on a surface (for jumping)
    public bool onPlatform = false; //flag to see if player is on a platform
    public float moveSpeed = 5f; //movespeed
    public float jumpForce = 3.5f; //regular jump

    public bool exploding = false;

    //additional distance to cast
    float extra = 0.1f;
   

    private Rigidbody2D rbody;
    private CapsuleCollider2D cCol;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        cCol = GetComponent<CapsuleCollider2D>();
    }

    // Fixed Update is called because physics calculations are required
    void FixedUpdate()
    {
        Move();
        CheckGrounded();
        NormalizeSpeed();
    }

    private void Update()
    {
        
        Jump();


    }

    void NormalizeSpeed()
    {
        if (!exploding)
        {
            if (Mathf.Abs(rbody.velocity.x) > MAX_HORIZONTAL_SPEED)
            {
                rbody.velocity = new Vector2(Mathf.Clamp(rbody.velocity.x, -MAX_HORIZONTAL_SPEED, MAX_HORIZONTAL_SPEED), rbody.velocity.y);
            }
        }


    }

    //Lateral Movement function
    void Move()
    {
        float x = Input.GetAxis("Horizontal");
        if (isGrounded)
        {
            rbody.AddForce(new Vector2(x * moveSpeed, 0));
        }
        else
        {
            rbody.AddForce(new Vector2(x*0.05f * moveSpeed, 0));
        }
        


    }

    void Jump()
    {
        //Adding force to obj for jump
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            rbody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (rbody.velocity.y < 0 && isGrounded == false)
        {
            rbody.gravityScale = 5;

        }
        else
        {
            rbody.gravityScale = 1;
        }

    }

    void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.transform.position, extra, platformLayer);
        Collider2D[] movingPlats = Physics2D.OverlapCircleAll(groundCheck.transform.position, extra, movingLayer);

        if (colliders.Length > 0 || movingPlats.Length > 0)
        {
            isGrounded = true;
            if(movingPlats.Length > 0)
            {
                transform.parent = movingPlats[0].transform;
            }
            else
            {
                transform.parent=null;
            }
           
        }
        else
        {
            isGrounded = false;
            transform.parent = null;
        }


    }
}