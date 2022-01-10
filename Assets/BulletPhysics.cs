using UnityEngine;
public class BulletPhysics : MonoBehaviour
{
    [Header("Bullet Physics")]
    [SerializeField] public float speed;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] public GameObject impactEffect;

    [Header("Bullet Config")]
    public int bulletDamage;
    public int bulletDuration;

    void Start()
    {
        rb.velocity = transform.right * speed;
        Destroy(gameObject, bulletDuration);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Regular Enemy Collision
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(bulletDamage);
            Instantiate(impactEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        if (collision.gameObject.layer.Equals("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
