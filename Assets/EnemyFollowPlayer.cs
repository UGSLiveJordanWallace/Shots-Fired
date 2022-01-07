using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public Transform player;
    [SerializeField] public float viewRange;
    [SerializeField] public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float playerDistance = Vector2.Distance(transform.position, player.position);
        if (playerDistance < viewRange) {
            StartFollowingPlayer();
        } else {
            StopFollowingPlayer();
        }
    }

    void StartFollowingPlayer() 
    {
        if (transform.position.x < player.position.x)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
            transform.localScale = new Vector2(3.120024f, 3.120024f);
        }
        else
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
            transform.localScale = new Vector2(-3.120024f, 3.120024f);
        }
    }

    void StopFollowingPlayer()
    {
        rb.velocity = Vector2.zero;
    }
}
