using UnityEngine.UI;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Essentials")]
    [SerializeField] public Transform firePoint;
    [SerializeField] public GameObject projectilePrefab;
    [SerializeField] public GameObject gunPrefab;
    [SerializeField] public GameObject firePrefab;

    [Header("Audio Clips")]
    [SerializeField] public AudioClip blastClip;
    [SerializeField] public AudioClip gunFireClip;
    [SerializeField] public AudioClip rpgClip;

    [Header("Animators")]
    [SerializeField] public Animator blastAnimator;

    // Reload and Ammunition
    [SerializeField] private Text ammoLeftText;
    [SerializeField] private Slider ammoLeftSlider;
    [SerializeField] private Image ammoLeftSliderImage;

    // Player Mouse Follow
    private Vector3 mousePos;
    private float angle;
    private float axis = 270;

    // Blaster Physics
    private const float BASIC_BLAST_TIME_TO_HOLD = 4.5f;
    private float currentHold;
    private float timeheld;
    private float timestart;
    private GameObject blastGo;

    // Assault Rifle physics
    private bool isShooting;
    private bool ShootingStopped;

    // Ammo Logic Variables
    private static bool isPickedUp;
    private int maxAmmunition;
    private int currentAmountOfAmmunition;

    private void Start()
    {
        // Gun Configuration Method OnStart
        GunConfig();
    }

    void Update()
    {
        // Gun Configuration OnUpdate
        GunConfigUpdate();
        
        // Cursor Position and Rotation values for Gun rotation
        mousePos = Input.mousePosition;
        mousePos.z = 0;
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        mousePos.x = objectPos.x - mousePos.x;
        mousePos.y = objectPos.y - mousePos.y;
        angle = Mathf.Atan2(-mousePos.x, mousePos.y) * Mathf.Rad2Deg;
        gunPrefab.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle + axis));

        // Gun Ammo Logic
        ammoLeftSlider.value = currentAmountOfAmmunition;
        ammoLeftText.text = currentAmountOfAmmunition.ToString();
        if (currentAmountOfAmmunition == (int)(maxAmmunition * .5))
        {
            ammoLeftSliderImage.color = Color.Lerp(Color.green, Color.yellow, 3f);
        }
        if (currentAmountOfAmmunition == (int)(maxAmmunition * .2))
        {
            ammoLeftSliderImage.color = Color.Lerp(Color.yellow, Color.red, 3f);
        }
        if (currentAmountOfAmmunition == maxAmmunition)
        {
            ammoLeftSliderImage.color = Color.Lerp(Color.red, Color.green, 3f);
        }

        // Shooting Mechanics and Gun Verification
        if (currentAmountOfAmmunition <= maxAmmunition && isPickedUp)
        {
            Reload();
            return;
        }
        if (currentAmountOfAmmunition <= 0)
        {
            return;
        }
        switch (gameObject.name)
        {
            case "Basic Pistol":
                if (Input.GetMouseButtonDown(0))
                {
                    BasicShot();
                    currentAmountOfAmmunition--;
                }
                break;
            case "Basic Blaster":
                if (Input.GetMouseButtonDown(0))
                {
                    BlastOnMouseDown();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    BlastOnMouseUp();
                    currentAmountOfAmmunition--;
                }
                break;
            case "Basic Uzi":
                if (isShooting)
                {
                    ShotVariatingFalse(0.14f);
                    if (!ShootingStopped)
                    {
                        BasicShot();
                        currentAmountOfAmmunition--;
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    ShootingStopped = false;
                    ShotVariatingTrue();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    isShooting = false;
                    ShootingStopped = true;
                }
                break;
            case "Basic Assault Rifle":
                if (isShooting)
                {
                    ShotVariatingFalse(0.2f);
                    if (!ShootingStopped)
                    {
                        BasicShot();
                        currentAmountOfAmmunition--;
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    ShootingStopped = false;
                    ShotVariatingTrue();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    isShooting = false;
                    ShootingStopped = true;
                }
                break;
            case "Kill Monger":
                if (isShooting)
                {
                    ShotVariatingFalse(0.09f);
                    if (!ShootingStopped)
                    {
                        BasicShot();
                        currentAmountOfAmmunition--;
                    }
                }
                if (Input.GetMouseButtonDown(0))
                {
                    ShootingStopped = false;
                    ShotVariatingTrue();
                }
                if (Input.GetMouseButtonUp(0))
                {
                    isShooting = false;
                    ShootingStopped = true;
                }
                break;
            case "Rocket Launcher":
                if (Input.GetMouseButtonDown(0))
                {
                    RPGFire();
                    currentAmountOfAmmunition--;
                }
                break;
        }
    }

    // Gun Configuration Update
    private void GunConfig()
    {
        switch (gameObject.name)
        {
            case "Basic Pistol":
                maxAmmunition = 15;
                break;
            case "Basic Assault Rifle":
                maxAmmunition = 30;
                break;
            case "Basic Blaster":
                maxAmmunition = 5;
                break;
            case "Basic Uzi":
                maxAmmunition = 60;
                break;
            case "Kill Monger":
                maxAmmunition = 120;
                break;
            case "Rocket Launcher":
                maxAmmunition = 5;
                break;
        }
        currentAmountOfAmmunition = maxAmmunition;
        ammoLeftSlider.maxValue = maxAmmunition;
        ammoLeftText.text = currentAmountOfAmmunition.ToString();
    }
    private void GunConfigUpdate()
    {
        ammoLeftSlider.maxValue = maxAmmunition;
        ammoLeftText.text = currentAmountOfAmmunition.ToString();
    }

    // Bullet Mechanics
    private void CreateProjectile()
    {
        // Bullet Creation
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Instantiate(firePrefab, firePoint.position, firePoint.rotation);
    }

    // Reload Mechanics
    private void Reload()
    {
        isPickedUp = false;
        currentAmountOfAmmunition = maxAmmunition;
        Debug.Log("Reloading...");
    }
    public static void setPickedUpTrue()
    {
        isPickedUp = true;
    }

    // Basic Shots
    void BasicShot()
    {
        // Shooting Logic
        BasicShotSoundEffect();
        CreateProjectile();
    }
    private void BasicShotSoundEffect()
    {
        PlayClipAtPoint(gunFireClip, transform.position);
    }

    // Machine Gun and Assualt Rifle Logic
    void ShotVariatingTrue()
    {
        isShooting = true;
    }
    void ShotVariatingFalse(float frequency)
    {
        isShooting = false;
        if (!ShootingStopped)
        {
            Invoke("ShotVariatingTrue", frequency);
        }
    }

    // Blast Mechanics
    private void BasicBlastMechanic()
    {
        blastAnimator.Play("basic-blast-on");
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        blastAnimator.Play("Basic Blast Idle");
    }
    private void BlastOnMouseUp()
    {
        currentHold = Time.time;
        timeheld = currentHold - timestart;
        if (timeheld >= BASIC_BLAST_TIME_TO_HOLD)
        {
            BasicBlastMechanic();
        }
        else
        {
            Destroy(blastGo);
            currentAmountOfAmmunition++;
            blastAnimator.Play("Basic Blast Idle");
        }
    }
    private void BlastOnMouseDown()
    {
        blastGo = new GameObject("PlayAndForget");
        blastGo.transform.position = transform.position;
        var blastAudioSource = blastGo.AddComponent<AudioSource>();
        blastAudioSource.clip = blastClip;
        Destroy(blastGo, blastClip.length);
        blastAudioSource.Play();
        blastAnimator.Play("basic-blast-animation");
        timestart = Time.time;
    }

    // RPG Mechanics
    private void RPGFire()
    {
        PlayClipAtPoint(rpgClip, transform.position);
        CreateProjectile();
    }
    
    // Sound Effect Mechanics
    public static AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position)
    {
        var go = new GameObject("PlayAndForget");
        go.transform.position = position;
        var audioSource = go.AddComponent<AudioSource>();
        audioSource.clip = clip;
        Destroy(go, clip.length);
        audioSource.Play();
        audioSource.volume = 0.35f;
        return audioSource;
    }

    // External Weapon
    public string GetWeaponName()
    {
        return gameObject.name;
    }
}
