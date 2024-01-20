using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip introClip;
    public AudioClip[] audioClips;

    private AudioSource audioSource;
    private int currentClipIndex = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Play the intro clip
        audioSource.clip = introClip;
        audioSource.Play();
        Invoke("StartPlaying", introClip.length); // Invoke StartPlaying after the intro clip finishes
    }

    void StartPlaying()
    {
        // Start cycling through the audio clips
        InvokeRepeating("PlayNextClip", 0f, audioClips[currentClipIndex].length);
    }

    void PlayNextClip()
    {
        // Stop the current clip
        audioSource.Stop();

        // Play the next clip in the array
        audioSource.clip = audioClips[currentClipIndex];
        audioSource.Play();

        // Increment the clip index, and loop back to the start if we reach the end of the array
        currentClipIndex = (currentClipIndex + 1) % audioClips.Length;
    }
}
