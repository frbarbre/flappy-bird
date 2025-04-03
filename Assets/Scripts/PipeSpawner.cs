using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public enum SpawnDirection
    {
        Top,
        Bottom
    }

    public GameObject pipePrefab;
    public float spawnRate = 2f;
    private float timer = 0f;

    public float maxY = 11f;
    public float minY = 4f;

    // Added new variable to determine spawn position
    private float rightEdge;

    public SpawnDirection spawnDirection = SpawnDirection.Top;
    private GameController gameController;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        // Get the right edge of the screen in world coordinates for spawning
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x + 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.currentGameState == GameController.GameState.Playing)
        {
            timer += Time.deltaTime;
            if (timer > spawnRate)
            {
                SpawnPipe();
                timer = 0f;
            }
        }
    }

    void SpawnPipe()
    {
        if (spawnDirection == SpawnDirection.Top)
        {
            float randomY = Random.Range(minY, maxY);
            // Spawn pipe from the top (flipped 180 degrees) at the right edge
            Instantiate(pipePrefab, new Vector3(rightEdge, randomY, 0), Quaternion.Euler(0, 0, 180f));
            spawnDirection = SpawnDirection.Bottom;
        }
        else
        {
            float randomY = Random.Range(minY * -1, maxY * -1);
            // Spawn pipe from the bottom (normal orientation) at the right edge
            Instantiate(pipePrefab, new Vector3(rightEdge, randomY, 0), Quaternion.identity);
            spawnDirection = SpawnDirection.Top;
        }
    }
}
