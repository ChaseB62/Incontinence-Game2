using UnityEngine;
//made by Aidem
public class SpikeScript : MonoBehaviour
{
    public int spikeDamage = 10;
    public Health playerHealthScript; // Reference to playerHealth script

    public AudioSource hitSource;
    public AudioClip hitClip;

    // Called when something enters the collider attached to this GameObject
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("canEffect"))
        {
            Debug.Log("collided");
            playerHealthScript = other.GetComponent<Health>();
            DecreaseHealth();

            hitSource.PlayOneShot(hitClip);
        }
    }

    // Method to decrease player's health
    private void DecreaseHealth()
    {
        if (playerHealthScript != null)
        {
            playerHealthScript.TakeDamage(spikeDamage);
            Debug.Log("Player health decreased by " + spikeDamage + ". Current health: " + playerHealthScript.startHealth);
        }
        else
        {
            Debug.LogError("PlayerHealth script not assigned to SpikeScript!");
        }
    }
}
