using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip introClip;
    public AudioClip loopingClip;

    private AudioSource audioSource;
    private bool introClipPlayed = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Set the intro audio clip
        audioSource.clip = introClip;

        // Start playing the intro audio
        audioSource.Play();
    }

    void Update()
    {
        // Check if the intro clip has finished playing
        if (!introClipPlayed && !audioSource.isPlaying)
        {
            // Intro clip has finished playing
            introClipPlayed = true;

            // Switch to the looping audio clip
            audioSource.clip = loopingClip;

            // Enable looping for the looping audio clip
            audioSource.loop = true;

            // Start playing the looping audio
            audioSource.Play();
        }
    }
}
