using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    [Header("Health: ")]
    [SerializeField] public int health;
    [SerializeField] public Slider healthBar;
    [SerializeField] public GameObject healthBarUI;
    [SerializeField] public GameObject player;

    [Header("Animations:")]
    [SerializeField] private Animator animator;

    [Header("Health Potion Prefabs: ")]
    [SerializeField] public GameObject maxPotion;
    [SerializeField] public GameObject halfPotion;
    [SerializeField] public GameObject quaterPotion;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckHealth();
    }

    // Checks to see if the Player is still alive
    private void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(player);
            SceneManager.LoadScene("You Died Scene");
        }

        if (health > 100)
        {
            health = 100;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Max Health Potion Fully Heals Player
        if (collision.gameObject.tag == "Max Health Potion")
        {
            FullHealth();
        }

        else if (collision.gameObject.tag == "Half Health Potion")
        {
            HalfHealth();
        }

        else if (collision.gameObject.tag == "Quarter Health Potion")
        {
            QuarterHealth();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EnemyBolder")
        {
            health -= 20;
            if (animator != null)
            {
                // play Bounce but start at a quarter of the way though
                animator.Play("player_healthBar_animation", 0, 0);
            }
            StartCoroutine(HandleHealthBarParameters()); ;
        }
    }

    // Health Potion Functionality
    private void FullHealth()
    {
        health = 100;
        Destroy(maxPotion);
        if (animator != null)
        {
            // play Bounce but start at a quarter of the way though
            animator.Play("player_healthBar_animation", 0, 0);
        }
        StartCoroutine(HandleHealthBarParameters());
    }

    private void HalfHealth()
    {
        Destroy(halfPotion);
        if (health >= 80)
        {
            health = 100;
        }

        if (health < 80 && health >= 60)
        {
            health += 20;
        }

        if (health >= 50 && health < 60)
        {
            health += 40;
        }

        if (health < 50)
        {
            health += 50;
        }

        if (animator != null)
        {
            // play Bounce but start at a quarter of the way though
            animator.Play("player_healthBar_animation", 0, 0);
        }

        StartCoroutine(HandleHealthBarParameters());
    }

    private void QuarterHealth()
    {
        Destroy(quaterPotion);
        if (health > 80)
        {
            health += 10;
        }

        if (health <= 80 && health > 60)
        {
            health += 15;
        }

        if (health <= 60 && health > 50)
        {
            health += 20;
        }

        if (health <= 50)
        {
            health += 35;
        }

        if (animator != null)
        {
            // play Bounce but start at a quarter of the way though
            animator.Play("player_healthBar_animation", 0, 0);
        }

        StartCoroutine(HandleHealthBarParameters());
    }

    // Handles The Health Bar Animation
    private IEnumerator HandleHealthBarParameters()
    {
        healthBar.value = health;
        healthBarUI.SetActive(true);
        yield return new WaitForSeconds(5);
        healthBarUI.SetActive(false);
    }
}
