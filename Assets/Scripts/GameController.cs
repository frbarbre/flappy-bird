using TMPro;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public enum GameState
    {
        Playing,
        GameOver
    }

    public GameState currentGameState = GameState.Playing;

    public GameObject player;

    public float score = 0;

    public TextMeshProUGUI scoreText;

    void Start()
    {
        scoreText = GameObject.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
