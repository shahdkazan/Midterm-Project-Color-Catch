using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;
    public AudioSource audioSource;
    public AudioClip musicClip;

    void Awake()
    {
       
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        //Do not be destroy when a color catch sence loads 
        DontDestroyOnLoad(gameObject);
    }

    //paly background music and loop
    public void PlayMusic()
    {
       
            audioSource.clip = musicClip;
            audioSource.loop = true;
            audioSource.Play();
        
    }

    //stop music when game over 
    public void StopMusic()
    {
        audioSource.Stop();
    }
}
