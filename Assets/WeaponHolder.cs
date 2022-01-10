using UnityEngine;

public class WeaponHolder : MonoBehaviour
{
    [Header("Weapon Holder")] 
    public GameObject weaponHolder;

    [Header("Current Weapon")]
    public GameObject currentWeapon;
    
    [Header("Loading Point For Guns")]
    public Transform localLoadPoint;

    private int totalWeapons = 1;
    private GameObject[] gunPrefabs;

    private void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;
        gunPrefabs = new GameObject[totalWeapons];

        for (int i = 0; i < gunPrefabs.Length; i++)
        {
            gunPrefabs[i] = weaponHolder.transform.GetChild(i).gameObject;
            gunPrefabs[i].SetActive(false);
            gunPrefabs[i].transform.position = localLoadPoint.position;
            
            if (gunPrefabs[i].name.Equals(PlayerPrefs.GetString("EQUP")))
            {
                currentWeapon = gunPrefabs[i];
                gunPrefabs[i].SetActive(true);
            }
        }
    }
}
