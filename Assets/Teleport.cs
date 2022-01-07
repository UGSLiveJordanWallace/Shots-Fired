using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] public Transform playerTransform;
    
    void Start()
    {
        playerTransform.position += new Vector3(30, 10, 0);
    }
}
