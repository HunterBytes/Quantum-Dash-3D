using UnityEngine;

public class PlayAudioSegment : MonoBehaviour
{
    private AudioSource audioSource;

    // Start and end times for the segment
    public float startTime = 1f; // Start at 1 second
    public float endTime = 3f;   // End at 3 seconds

    private void Start()
    {
        // Get the AudioSource component attached to the player
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Debug log to check what the player is colliding with
        Debug.Log("Collided with: " + collision.gameObject.name);

        // Check if the colliding object is tagged as "Obstacle"
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            // Play the specified segment of the audio if it is not already playing
            if (!audioSource.isPlaying)
            {
                PlaySegment();
            }
        }
    }

    private void PlaySegment()
    {
        if (audioSource != null)
        {
            // Set the start time of the audio clip
            audioSource.time = startTime;

            // Play the audio clip from the specified start time
            audioSource.Play();

            // Stop the audio after the specified duration
            Invoke("StopAudio", endTime - startTime);
        }
    }

    private void StopAudio()
    {
        if (audioSource != null && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}