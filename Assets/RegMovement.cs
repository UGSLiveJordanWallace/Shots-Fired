using UnityEngine;

public class RegMovement : MonoBehaviour
{
    [Header("Player Physics")]
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] float jumpForce;
    public float speed;

    [Header("Player Bools, Layers, and Transforms")]
    public float checkRadius;
    public float jumpTime;
    public LayerMask whatisground;
    public Transform feetPos;
    public bool facingRight;

    private float movement;
    private bool isJumping;
    private bool isGrounded;
    private float jumpTimeCounter;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        facingRight = false;
    }

    void Update()
    {
        JumpingMechanic();

        movement = Input.GetAxisRaw("Horizontal");
    }

    private void FixedUpdate()
    {
        playerRB.velocity = new Vector2(movement * speed * Time.deltaTime, playerRB.velocity.y);
        if (facingRight == false && movement < 0)
        {
            Flip();
        }
        else if (facingRight == true && movement > 0)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    private void JumpingMechanic()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatisground);

        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRB.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.Space) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                playerRB.velocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
        }
    }
}
