using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Power Ups")]
    [SerializeField] private GameObject buildUpRef;
    [SerializeField] private GameObject blinkRef;
    [SerializeField] private GameObject dashUpRef;

    [Header("Audio Sources")]
    [SerializeField] private AudioClip winClip;

    [Header("UI Elements")]
    [SerializeField] public GameObject tryAgainPrefab;
    [SerializeField] public GameObject nextLevelPrefab;
    [SerializeField] public Text pointUI;
    [SerializeField] public GameObject pointUIObject;
    [SerializeField] public SpriteRenderer spriteRenderer;

    // Points
    private int points;

    // Level Completed
    private bool levelCompleted = false;

    // Processes
    private void Start()
    {
        levelCompleted = false;
        print(GetPoints());
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = CharacterSelector.spriteRenderer;
    }
    void Update()
    {
        InputProcessing();

        // The altitude of the player
        OnDepthEnter(transform.position.y);
    }
    void InputProcessing()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            Application.Quit();
        }
    }

    // Point Handlers
    public void SendDeathMessage(int points)
    {
        Points(points);
    }
    public void Points(int point)
    {
        points += point;
        pointUI.text = points.ToString();
    }
    void OnDepthEnter(float y)
    {
        if (y <= -30 || y >= 120)
        {
            Invoke("Restart", .2f);
        }
    }

    // OnCollision and OnTrigger Event Handlers
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "BuildPowerUp")
        {
            Camera.main.GetComponent<Build>().enabled = true;
            Destroy(buildUpRef);
        }
        if (collision.gameObject.tag == "BlinkPowerUp")
        {
            Camera.main.GetComponent<Teleport>().enabled = true;
            Destroy(blinkRef);
        }
        if (collision.gameObject.tag == "DashPowerUp")
        {
            gameObject.GetComponent<RegMovement>().enabled = false;
            gameObject.GetComponent<DashMovement>().enabled = true;
            Destroy(dashUpRef);
        }
        if (collision.gameObject.tag == "Ammo")
        {
            Weapon.setPickedUpTrue();
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Landing")
        {
            if (!levelCompleted)
            {
                levelCompleted = true;
                Landing();
            }
        }
        else if (collision.gameObject.tag == "Untagged")
        {
            tryAgainPrefab.SetActive(false);
            nextLevelPrefab.SetActive(false);
        }
    }

    // GamePlay
    void Restart()
    {
        SceneManager.LoadScene("You Died Scene");
    }
    private void Landing()
    {
        PlayClipAtPoint(winClip, transform.position);
        tryAgainPrefab.SetActive(true);
        nextLevelPrefab.SetActive(true);
        if (CheckIfLocalStorageExists())
        {
            AddToCurrentPoints(int.Parse(pointUI.text));
        } else
        {
            savePoints();
        }
    }

    // Audio
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

    // Point LocalStorage
    public void savePoints()
    {
        PlayerPrefs.SetInt("UPS", int.Parse(pointUI.text));
        PlayerPrefs.Save();
    }
    public void AddToCurrentPoints(int deposit)
    {
        if (CheckIfLocalStorageExists())
        {
            int currentPoints = GetPoints();
            print(currentPoints);
            currentPoints += deposit;
            PlayerPrefs.SetInt("UPS", currentPoints);
        } else
        {
            savePoints();
        }
    }
    public bool CheckIfLocalStorageExists()
    {
        return PlayerPrefs.HasKey("UPS");
    }
    public int GetPoints()
    {
        return PlayerPrefs.GetInt("UPS");
    }
}
