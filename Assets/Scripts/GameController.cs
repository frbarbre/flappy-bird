using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum GameState
    {
        Playing,
        Paused,
        GameOver
    }

    public GameState currentGameState = GameState.Playing;

    public GameObject player;

    public float score = 0;
    private float highScore = 0;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public GameObject gameOverScreen;
    public GameObject pausedScreen;

    public AudioSource backgroundMusic;


    void Start()
    {
        gameOverScreen.SetActive(false);
        pausedScreen.SetActive(false);

        // Load the high score from PlayerPrefs
        highScore = PlayerPrefs.GetFloat("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();

        backgroundMusic.Play();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        currentGameState = GameState.Paused;
        gameOverScreen.SetActive(false);
        pausedScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        currentGameState = GameState.Playing;
        gameOverScreen.SetActive(false);
        pausedScreen.SetActive(false);
    }

    public void GameOver()
    {
        currentGameState = GameState.GameOver;
        gameOverScreen.SetActive(true);
        pausedScreen.SetActive(false);
        backgroundMusic.Stop();

        // Save high score when game ends
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
            PlayerPrefs.Save();
        }
    }

    // Add a method to reset the high score (useful for testing)
    public void ResetHighScore()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore = 0;
        if (highScoreText != null)
        {
            highScoreText.text = "High Score: 0";
        }
    }
}
