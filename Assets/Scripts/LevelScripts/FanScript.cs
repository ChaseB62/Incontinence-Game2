//written by Aiden
using UnityEngine;

public class FanScript : MonoBehaviour
{
    public float continuousForce = 5f; // Adjust the force as needed

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("canEffect"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // Apply continuous upward force to the player when they stay in the trigger
                playerMovement.rb.AddForce(Vector2.up * continuousForce, ForceMode2D.Force);
                
            }
        }
    }
}
