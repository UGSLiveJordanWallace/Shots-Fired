using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    private int totalWeapons = 1;
    public int currentWeaponIndex;
    
    [Header("Weapon Holder")]
    [SerializeField] GameObject weaponHolder;

    [Header("Current Weapon")]
    [SerializeField] GameObject currentWeapon;
    
    [Header("Gun Prefabs")]
    [SerializeField] GameObject[] gunPrefabs;

    [Header("Loading Point For Guns")]
    public Transform localLoadPoint;

    private void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        gunPrefabs = new GameObject[totalWeapons];

        for (int i = 0; i < gunPrefabs.Length; i++)
        {
            gunPrefabs[i] = weaponHolder.transform.GetChild(i).gameObject;
            gunPrefabs[i].SetActive(false);
            gunPrefabs[i].transform.position = localLoadPoint.position;
            
            switch (gunPrefabs[i].name)
            {
                case "Basic Pistol":
                    gunPrefabs[i].transform.localScale = new Vector3(0.2419158f, 0.2253732f, 0.2145595f);
                    break;
                case "Basic Assault":
                    gunPrefabs[i].transform.localScale = new Vector3(0.4085456f, 0.4085456f, 0.4085456f);
                    break;
            }
            currentWeapon = gunPrefabs[0];
            currentWeaponIndex = 0;
        }
        gunPrefabs[0].SetActive(true);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            if (currentWeaponIndex < totalWeapons - 1)
            {
                gunPrefabs[currentWeaponIndex].SetActive(false);
                currentWeaponIndex += 1;
                currentWeapon = gunPrefabs[currentWeaponIndex];
                gunPrefabs[currentWeaponIndex].SetActive(true);
            }
            else if (currentWeaponIndex == totalWeapons - 1)
            {
                gunPrefabs[currentWeaponIndex].SetActive(false);
                currentWeaponIndex = 0;
                currentWeapon = gunPrefabs[currentWeaponIndex];
                gunPrefabs[currentWeaponIndex].SetActive(true);
            }
        }
    }
}
