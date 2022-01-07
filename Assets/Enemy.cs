using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int health;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] public GameObject deathEffectPrefab;
    [SerializeField] public AudioClip dieClip;

    // Player Object
    [SerializeField] private Player player;

    // Various Points
    private int basic = 5;
    private int boss = 30;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            player.SendDeathMessage(basic);
            Instantiate(deathEffectPrefab, gameObject.transform.position, gameObject.transform.rotation);
            PlayClipAtPoint(dieClip, transform.position);
            Destroy(gameObject);
        }
    }
    public static AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position)
    {
        var go = new GameObject("PlayAndForget");
        go.transform.position = position;
        var audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        Destroy(go, clip.length);
        audioSource.Play();
        return audioSource;
    }
}
