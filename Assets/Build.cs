using UnityEngine.UI;
using UnityEngine;

public class Build : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform prefabTransform;
    [SerializeField] Text text;
    private int boldersLeft = 100;

    void Update()
    {
        Vector3 cursor = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        cursor.z = 0;

        if (Input.GetButtonDown("Fire2") && boldersLeft > 0)
        {
            Instantiate(prefab, cursor, prefabTransform.rotation);
            boldersLeft -= 1;

            //Setting the UI text element
            text.text = boldersLeft.ToString();
        }
        
        if(boldersLeft < 1)
        {
            text.text = "";
            GetComponent<Build>().enabled = false;
        }
    }
}
