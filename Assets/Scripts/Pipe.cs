using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float moveSpeed = 5f;
    private float leftEdge;

    private GameController gameController;

    private void Start()
    {
        // Find the left edge of the screen in world coordinates
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x - 1f;
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        if (gameController.currentGameState == GameController.GameState.Playing)
        {
            // Move the pipe to the left
            transform.position += Vector3.left * moveSpeed * Time.deltaTime;

            // If the pipe has moved beyond the left edge of the screen, destroy it
            if (transform.position.x < leftEdge)
            {
                Destroy(gameObject);
            }
        }
    }
}