using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement; // Needed for scene loading


public class MenuController : MonoBehaviour
{
    //public AudioSource audioSource;
    //public AudioClip backgroundMusic;

    private void Start()
    {
        MusicPlayer.Instance.PlayMusic();
    }
    public void StartGame()
    {
        // Load the game scene
        SceneManager.LoadScene("Color Catch");
    }

    public void QuitGame()
    {
        // Quit the application (works in build, not editor)
        Application.Quit();
        Debug.Log("Game Quit");
    }
}

