using UnityEngine;

public class PersistentAudio : MonoBehaviour
{
    private static PersistentAudio instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Prevent this GameObject from being destroyed on scene change
        }
        else
        {
            Destroy(gameObject); // Ensure only one instance of this object exists
        }
    }

    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play(); // Start playing music if not already playing
        }
    }
}
