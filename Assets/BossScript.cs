using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] Transform BossTransform;

    public Vector3 offset;

    void Update()
    {
        offset.z = 0;
        transform.position = player.transform.position + offset;
    }
}
