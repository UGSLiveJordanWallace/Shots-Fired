using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class DashMovement : MonoBehaviour
{
    [Header("Player Physics")]
    [SerializeField] Rigidbody2D playerRB;
    [SerializeField] float jumpForce;
    public float speed;
    public float dashSpeed;

    [Header("Player Bools, Layers, and Transforms")]
    public float checkRadius;
    public float jumpTime;
    public LayerMask whatisground;
    public Transform feetPos;
    public bool facingRight;
    public const float DASH_TIME_THRESHOLD = 0.5f;
    public const float DASH_COOL_DOWN= 5f;

    [Header("Camera")]
    [SerializeField] private Camera cam;

    [Header("Particle System")]
    [SerializeField] private ParticleSystem ps;

    [Header("Dash UI")]
    [SerializeField] private GameObject dashUIObj;
    [SerializeField] private Slider dashSlider;

    private float movement;
    private bool isJumping;
    private bool isGrounded;
    private float jumpTimeCounter;
    private float lastTapped;
    private bool isDashing = false;
    private KeyCode prevKeyCode;
    private float timeSinceDashTap;
    private float dashCoolDownLimit;

    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
        facingRight = false;
    }

    void Update()
    {
        float currentCoolDown = Time.time - timeSinceDashTap;
        JumpingMechanic();
        DashMechanic();
        movement = Input.GetAxisRaw("Horizontal");

        HandleDashUI(currentCoolDown);
    }

    private void FixedUpdate()
    {
        if (!isDashing)
        {
            playerRB.gravityScale = 5;
            playerRB.velocity = new Vector2(movement * speed * Time.deltaTime, playerRB.velocity.y);
        }
        if (facingRight == false && movement < 0)
        {
            Flip();
        }
        else if (facingRight == true && movement > 0)
        {
            Flip();
        }
    }

    private void Flip()
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

    private void DashMechanic()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            float timeDifference = Time.time - lastTapped;
            dashCoolDownLimit = Time.time - timeSinceDashTap;
            if (timeDifference < DASH_TIME_THRESHOLD && prevKeyCode == KeyCode.A && dashCoolDownLimit > DASH_COOL_DOWN)
            {
                StartCoroutine(Dash(-1f));
            }
            else
            {
                prevKeyCode = KeyCode.A;
            }

            lastTapped = Time.time;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            float timeDifference = Time.time - lastTapped;
            dashCoolDownLimit = Time.time - timeSinceDashTap;
            if (timeDifference < DASH_TIME_THRESHOLD && prevKeyCode == KeyCode.D && dashCoolDownLimit > DASH_COOL_DOWN)
            {
                StartCoroutine(Dash(1f));
            }
            else
            {
                prevKeyCode = KeyCode.D;
            }

            lastTapped = Time.time;
        }
    }
    private IEnumerator Dash(float dashDirection)
    {
        timeSinceDashTap = Time.time;
        ps.Play();
        isDashing = true;
        playerRB.velocity = new Vector2(playerRB.velocity.x, 0f);
        playerRB.AddForce(new Vector2(dashDirection * dashSpeed * Time.deltaTime, 0f), ForceMode2D.Impulse);
        float gravity = playerRB.gravityScale;
        playerRB.gravityScale = 0;

        float elapsed = 0.0f;
        while (elapsed < 0.6f)
        {
            float x = Random.Range(-1f, 1f) * 1.2f;
            float y = Random.Range(-1f, 1f) * 1.2f;

            cam.transform.localPosition = new Vector3(cam.transform.position.x + x, cam.transform.position.y + y, cam.transform.position.z);
            elapsed += Time.deltaTime;

            yield return null;
        }

        ps.Stop();
        isDashing = false;
        playerRB.gravityScale = gravity;
    }

    private void HandleDashUI(float coolDown)
    {
        dashUIObj.SetActive(true);

        dashSlider.value = coolDown * 20f / 100f;
    }
}
