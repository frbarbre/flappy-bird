using NUnit.Framework;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpVelocity = 4f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private GameController gameController;

    // Added rotation parameters
    public float maxRotationAngle = 35f;
    public float rotationSpeed = 10f;
    private float targetRotation = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameController.currentGameState == GameController.GameState.Playing)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Reset the vertical velocity before applying the jump force
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
                rb.linearVelocity = Vector2.up * jumpVelocity;
                // Set upward rotation when jumping
                targetRotation = maxRotationAngle;
            }

            // Calculate target rotation based on linearVelocity
            if (rb.linearVelocity.y < 0)
            {
                // Falling - rotate downward faster
                targetRotation = -maxRotationAngle;
            }

            // Smoothly interpolate current rotation to target rotation
            float currentRotation = transform.rotation.eulerAngles.z;
            if (currentRotation > 180) currentRotation -= 360; // Normalize angle
            float newRotation = Mathf.Lerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Euler(0, 0, newRotation);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Death")
        {
            KillPlayer();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Death")
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        gameController.currentGameState = GameController.GameState.GameOver;
        spriteRenderer.color = new Color(217 / 255f, 1 / 255f, 1 / 255f);
    }
}
