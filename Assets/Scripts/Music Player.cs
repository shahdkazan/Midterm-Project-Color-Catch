using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;
    public AudioSource audioSource;
    public AudioClip musicClip;

    void Awake()
    {
        // keep only one music object
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMusic()
    {
       
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.Play();
        
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
