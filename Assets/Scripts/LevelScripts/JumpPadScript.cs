using UnityEngine;
using System.Collections;

// Made by Aiden
public class JumpPadScript : MonoBehaviour
{
    public float jumpVelocity = 10f;
    public float flattenAmount = 0.5f; // Adjust this value to control how much the frog flattens
    public float transitionDuration = 0.5f; // Adjust this value to control the duration of flattening and restoring

    public AudioSource jumpSource;
    public AudioClip jumpClip;

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

            if (playerMovement != null)
            {
                // Set upward velocity to the player when they touch the jump pad
                playerMovement.rb.velocity = new Vector2(playerMovement.rb.velocity.x, jumpVelocity);
                jumpSource.PlayOneShot(jumpClip);

                // Flatten the frog and then restore
                StartCoroutine(FlattenAndRestore());
            }
        }
    }

    IEnumerator FlattenAndRestore()
    {
        Vector3 targetFlattenScale = new Vector3(originalScale.x, originalScale.y - flattenAmount, originalScale.z);

        // Glide to the flattened scale
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetFlattenScale, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it's set to the exact flattened scale
        transform.localScale = targetFlattenScale;

        // Wait for a short duration
        yield return new WaitForSeconds(0.5f); // Adjust the duration as needed

        // Glide back to the original scale
        elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            transform.localScale = Vector3.Lerp(targetFlattenScale, originalScale, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure it's set to the exact original scale
        transform.localScale = originalScale;
    }
}
