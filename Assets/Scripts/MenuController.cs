using UnityEngine;
using UnityEngine.SceneManagement; 


public class MenuController : MonoBehaviour
{
    //call play music function
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

