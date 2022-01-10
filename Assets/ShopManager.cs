using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private Button buy;
    [SerializeField] private Button equip;
    [SerializeField] private Text pointUI;
    [SerializeField] private int cost;

    private Text buttonChildTextGameObject;
    private Text equipChildTextGameObject;
    public string gunName;
    private int currentPoints;

    private void Start()
    {
        print(PlayerPrefs.GetString("EQUP"));
        currentPoints = PlayerPrefs.GetInt("UPS");
        buttonChildTextGameObject = buy.GetComponentInChildren<Text>();
        equipChildTextGameObject = equip.GetComponentInChildren<Text>();
        if (PlayerPrefs.HasKey(gunName) && PlayerPrefs.GetInt(gunName) == 1) 
        {
            buttonChildTextGameObject.text = "Bought";
            ChangeButtonColor(buy, Color.black);
        }
        if (PlayerPrefs.GetString("EQUP").Equals(gunName))
        {
            equipChildTextGameObject.text = "Unequip";
        }
        pointUI.text = currentPoints.ToString();
    }

    public void Buy()
    {
        if (currentPoints >= cost)
        {
            currentPoints -= cost;
            buttonChildTextGameObject.text = "Bought";
            buy.interactable = false;
            print(currentPoints);
            print(buttonChildTextGameObject.text);
            PlayerPrefs.SetInt("UPS", currentPoints);
            PlayerPrefs.SetInt(gunName, 1);
        }
    }

    public void Equip()
    {
        if (PlayerPrefs.HasKey(gunName) && PlayerPrefs.GetInt(gunName) == 1 && PlayerPrefs.GetString("EQUP").Equals(gunName))
        {
            equipChildTextGameObject.text = "Equip";
            PlayerPrefs.SetString("EQUP", "Basic Pistol");
        } else if (PlayerPrefs.HasKey(gunName) && PlayerPrefs.GetInt(gunName) == 1)
        {
            equipChildTextGameObject.text = "Unequip";
            PlayerPrefs.SetString("EQUP", gunName);
        }
    }

    public Button ChangeButtonColor(Button button, Color color)
    {
        ColorBlock cb = button.colors;
        cb.normalColor = color;
        button.colors = cb;
        return button;
    }
}
