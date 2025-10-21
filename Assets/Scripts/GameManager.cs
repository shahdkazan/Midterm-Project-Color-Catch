using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // for restart

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Color[] possibleColors = { Color.red, Color.green, Color.yellow };
    public Color targetColor;
    public TextMeshProUGUI targetColorText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;

    public GameObject gameOverUI;
    public TextMeshProUGUI finalScoreText;


    public int score = 0;
    public float timeRemaining = 20f;   // 60 seconds
    private bool gameOver = false;

    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    public AudioClip winClip;
    public AudioClip loseClip;


    void Awake() => Instance = this;

    void Start()
    {
        PickNewTargetColor();
        UpdateUI();
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
    }

    void Update()
    {
        if (gameOver) return;

        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            EndGame();
        }
        UpdateUI();
    }

    public void PickNewTargetColor()
    {
        targetColor = possibleColors[Random.Range(0, possibleColors.Length)];
        targetColorText.text = "Target: " + ColorName(targetColor);
        targetColorText.color = targetColor;
    }

    public void HandlePickup(Color pickupColor)
    {
        if (gameOver) return;

        if (pickupColor == targetColor)
            score += 10;
        else
            score -= 5;

        UpdateUI();
    }

    void UpdateUI()
    {
        scoreText.text = "Score: " + score;
        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);
    }

    //void EndGame()
    //{
    //    gameOver = true;
    //    timerText.text = "Time: 0";
    //    targetColorText.text = "GAME OVER";
    //    targetColorText.color = Color.white;
    //    // optional: restart after 3 sec
    //    Invoke(nameof(RestartGame), 3f);
    //}

    void EndGame()
    {
        gameOver = true;
        timerText.text = "Time: 0";
        targetColorText.text = "";
        //scoreText.text = "";
        scoreText.gameObject.SetActive(false);
        gameOverUI.SetActive(true);
        finalScoreText.text = "Final Score: " + score;

        audioSource.Stop();

        if (score > 0)
            audioSource.PlayOneShot(winClip);
        else
            audioSource.PlayOneShot(loseClip);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    string ColorName(Color c)
    {
        if (c == Color.red) return "Red";
        if (c == Color.green) return "Green";
        if (c == Color.yellow) return "Yellow";
        return "Unknown";
    }
}
