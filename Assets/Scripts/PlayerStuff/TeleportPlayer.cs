using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    private Vector3 teleportPosition = new Vector3(0, 0, 0);

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered Trigger");
        if (other.CompareTag("Teleport"))
        {
            transform.position = teleportPosition;
            Debug.Log("Player teleported");
        }
    }
}
