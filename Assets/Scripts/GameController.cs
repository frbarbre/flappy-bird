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

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
