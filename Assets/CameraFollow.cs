using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Vector3 camPosition;
    [SerializeField] GameObject player;

    void Update()
    {
        Vector3 playerPosition = new Vector3(player.transform.position.x + camPosition.x, camPosition.y, camPosition.z);
        transform.position = Vector3.Lerp(playerPosition, player.transform.position, .15f);
    }
}
