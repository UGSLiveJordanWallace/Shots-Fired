using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private GameObject characterModel;
    [SerializeField] private GameObject[] characters;
    [SerializeField] public static Sprite spriteRenderer;

    private int currentCharacterIndex;

    private void Awake()
    {
        characters = new GameObject[characterModel.transform.childCount];
        for (int i = 0; i < characters.Length; i++)
        {
            characters[i] = characterModel.transform.GetChild(i).gameObject;
            characters[i].SetActive(false);
            spriteRenderer = characters[0].GetComponent<SpriteRenderer>().sprite;
            currentCharacterIndex = 0;
        }
        characters[0].SetActive(true);
    }

    public void NextCharacter()
    {
        if (currentCharacterIndex < characters.Length - 1)
        {
            characters[currentCharacterIndex].SetActive(false);
            currentCharacterIndex += 1;
            spriteRenderer = characters[currentCharacterIndex].GetComponent<SpriteRenderer>().sprite;
            characters[currentCharacterIndex].SetActive(true);
        } else if (currentCharacterIndex == characters.Length - 1)
        {
            characters[currentCharacterIndex].SetActive(false);
            currentCharacterIndex = 0;
            spriteRenderer = characters[currentCharacterIndex].GetComponent<SpriteRenderer>().sprite;
            characters[currentCharacterIndex].SetActive(true);
        }
    }

    public void PreviousCharacter() 
    {
        if (currentCharacterIndex > 0)
        {
            characters[currentCharacterIndex].SetActive(false);
            currentCharacterIndex -= 1;
            spriteRenderer = characters[currentCharacterIndex].GetComponent<SpriteRenderer>().sprite;
            characters[currentCharacterIndex].SetActive(true);
        }
        else if (currentCharacterIndex == 0)
        {
            characters[currentCharacterIndex].SetActive(false);
            currentCharacterIndex = characters.Length - 1;
            spriteRenderer = characters[currentCharacterIndex].GetComponent<SpriteRenderer>().sprite;
            characters[currentCharacterIndex].SetActive(true);
        }
    }
}
