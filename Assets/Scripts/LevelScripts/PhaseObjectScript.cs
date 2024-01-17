using System.Collections;
using UnityEngine;

//made by Aiden
public class PhaseObjectScript : MonoBehaviour
{
    public float fadeTime = 3f; // Adjust the fade duration as needed
    public float finalAlpha = 0.3f; // Adjust the final alpha value as needed
    public float delayBetweenPhases = 2f; // Adjust the delay between fade-out and fade-in
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        // Start the fading loop after a delay (e.g., 2 seconds)
        StartCoroutine(FadingLoopCoroutine(2f));
    }

    IEnumerator FadingLoopCoroutine(float initialDelay)
    {
        while (true)
        {
            // Wait for the initial delay before starting each phase
            yield return new WaitForSeconds(initialDelay);

            // Fade out
            yield return StartCoroutine(FadeOutCoroutine());

            // Wait for the delay between phases
            yield return new WaitForSeconds(delayBetweenPhases);

            // Fade in
            yield return StartCoroutine(FadeInCoroutine());
        }
    }

    IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;
        float initialAlpha = startColor.a;

        while (elapsedTime < fadeTime)
        {
            // Gradually decrease alpha over time
            float newAlpha = Mathf.Lerp(initialAlpha, finalAlpha, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the final alpha is set to the specified value
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, finalAlpha);

        // Disable the box collider after fading
        boxCollider.enabled = false;
    }

    IEnumerator FadeInCoroutine()
    {
        float elapsedTime = 0f;
        Color startColor = spriteRenderer.color;

        while (elapsedTime < fadeTime)
        {
            // Gradually increase alpha over time
            float newAlpha = Mathf.Lerp(finalAlpha, 1f, elapsedTime / fadeTime);
            spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the alpha is set to 1 (fully opaque)
        spriteRenderer.color = new Color(startColor.r, startColor.g, startColor.b, 1f);

        // Enable the box collider after fading
        boxCollider.enabled = true;
    }
}
