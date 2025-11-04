using UnityEngine;
using TMPro;                  // For handling TextMeshPro UI text
using UnityEngine.SceneManagement; // For scene reload (restart)

public class GameManager : MonoBehaviour
{
    // Singleton instance so other scripts can easily access GameManager
    public static GameManager Instance;

    // Array of possible colors for pickups
    private Color[] possibleColors = { Color.red, Color.green, Color.yellow };
    private Color targetColor;  // The current target color player should collect

    // UI Elements
    public GameObject gameOverUI;            
    public TextMeshProUGUI finalScoreText;   
    public TextMeshProUGUI GameOverText;     
    public TextMeshProUGUI targetColorText;  
    public TextMeshProUGUI scoreText;       
    public TextMeshProUGUI timerText;        

    // Audio
    public AudioSource audioSource; 
    public AudioClip correctSound;  
    public AudioClip wrongSound;    
    public AudioClip winClip;       
    public AudioClip loseClip;      

    // References to in-game objects
    public GameObject enemy;
    public GameObject player;

    // Game state variables
    private int score = 0;           // Player score
    public float timeRemaining;      // Countdown timer
    private bool gameOver = false;   // Tracks if the game is over

    // Awake is called before Start used to setup singleton
    void Awake()
    {
        if (Instance == null)
            Instance = this; // Set singleton instance
        else
            Destroy(gameObject); // Destroy duplicates
    }

   
    void Start()
    {
        PickNewTargetColor(); // Choose the first target color
        UpdateUI();           // Initialize UI
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver) return; // Stop timer if game is over

        // Decrease remaining time
        timeRemaining -= Time.deltaTime;

        // If time runs out, end the game
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndGame();
        }

        // Update UI every frame
        UpdateUI();
    }

    // Picks a new target color randomly from possibleColors
    public void PickNewTargetColor()
    {
        targetColor = possibleColors[Random.Range(0, possibleColors.Length)];
        targetColorText.text = "Target: " + ColorName(targetColor);
        targetColorText.color = targetColor;
    }

    // Called when the player picks up a color
    public void HandlePickup(Color pickupColor)
    {
        if (gameOver) return;

        if (pickupColor == targetColor)
        {
            score += 10; // Increase score for correct pickup
            audioSource.PlayOneShot(correctSound);
        }
        else
        {
            score -= 5; // Decrease score for wrong pickup
            audioSource.PlayOneShot(wrongSound);
        }

        UpdateUI(); // Update score display
    }

    // Updates score and timer UI
    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
    }

    // Handles end-of-game logic
    void EndGame()
    {
        gameOver = true;

        // Hide gameplay UI
        timerText.text = "Time: 0";
        targetColorText.text = "";
        scoreText.gameObject.SetActive(false);

        // Show game over UI
        gameOverUI.SetActive(true);
        finalScoreText.text = "Final Score: " + score;

        // Stop background music
        MusicPlayer.Instance.StopMusic();

        // Show win/lose messages
        if (score > 0)
        {
            audioSource.PlayOneShot(winClip);
            GameOverText.text = "GAME OVER YOU WIN";
            GameOverText.color = Color.green;
        }
        else
        {
            audioSource.PlayOneShot(loseClip);
            GameOverText.text = "GAME OVER YOU LOSE";
            GameOverText.color = Color.red;
        }

        // Destroy player and enemy objects
        if (enemy != null && player != null)
        {
            Destroy(enemy);
            Destroy(player);
        }
    }

    // Restart the current scene (game)
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        MusicPlayer.Instance.PlayMusic(); // Restart background music
    }

    // Returns the name of a color for UI display
    string ColorName(Color c)
    {
        if (c == Color.red) return "Red";
        if (c == Color.green) return "Green";
        if (c == Color.yellow) return "Yellow";
        return "Unknown";
    }

    // Reduce score
    public void ReduceScore()
    {
        if (gameOver) return;

        score -= 10;
        UpdateUI();
    }
}
