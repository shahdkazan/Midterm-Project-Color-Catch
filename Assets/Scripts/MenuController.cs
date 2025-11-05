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

   
}

