using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    private Vector3 teleportPosition = new Vector3(0, 0, 0);

    private Rigidbody2D rb;

    private void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Entered Trigger");
        if (other.CompareTag("Teleport"))
        {
            transform.position = teleportPosition;
            rb.velocity = new Vector2(0f, 0f);
            Debug.Log("Player teleported");
        }
    }
}
