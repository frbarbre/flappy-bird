using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    public GameObject pipePrefab;
    public float spawnRate = 2f;
    private float timer = 0f;

    // Gap size between pipes (vertical space for the bird to fly through)
    public float gapSize = 4f;

    // Range for the gap's vertical position
    public float maxGapY = 3f;
    public float minGapY = -3f;

    // Right edge of the screen for spawning
    private float rightEdge;

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
                SpawnPipePair();
                timer = 0f;
            }
        }
    }

    void SpawnPipePair()
    {
        // Random position for the center of the gap
        float gapCenter = Random.Range(minGapY, maxGapY);

        // We need to know the height of our pipe prefab to position correctly
        // This assumes you've measured the height of your pipe prefab
        float pipeHeight = 10f; // Adjust this value based on your actual pipe prefab height

        // Position the top pipe so its bottom edge is at gapCenter + halfGapSize
        float topPipeY = gapCenter + gapSize + (pipeHeight / 2);

        // Position the bottom pipe so its top edge is at gapCenter - halfGapSize
        float bottomPipeY = gapCenter - gapSize - (pipeHeight / 2);

        // Create top pipe (flipped 180 degrees)
        Instantiate(pipePrefab, new Vector3(rightEdge, topPipeY, 0), Quaternion.Euler(0, 0, 180f));

        // Create bottom pipe (normal orientation)
        GameObject bottomPipe = Instantiate(pipePrefab, new Vector3(rightEdge, bottomPipeY, 0), Quaternion.identity);

        GameObject bottomPipeTrigger = bottomPipe.transform.Find("PointTrigger").gameObject;
        Destroy(bottomPipeTrigger);
    }
}
