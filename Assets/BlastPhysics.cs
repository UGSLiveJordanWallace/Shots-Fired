using System.Collections;
using UnityEngine;

public class BlastPhysics : MonoBehaviour
{
    public int blastDamage = 400;
    void Start()
    {
        StartCoroutine(CreateBasicBlast());   
    }

    private IEnumerator CreateBasicBlast()
    {
        yield return new WaitForSeconds(3);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Regular Enemy Collision
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(blastDamage);
        }
    }
}
