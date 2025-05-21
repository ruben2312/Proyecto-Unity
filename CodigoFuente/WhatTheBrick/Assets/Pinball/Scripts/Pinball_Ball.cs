using UnityEngine;
using UnityEngine.SceneManagement; // Required for scene management

public class Pinball_Ball : MonoBehaviour
{
    public GameObject ballPrefab; // Reference to the ball prefab
    public AudioClip brickBreakSound;
    public AudioClip bumberSound;
    public AudioClip deathSound;
    public GameObject brickBreakParticle;
    private AudioSource audioSource; // Reference to the AudioSource component
    private Vector3 startPos;
    private Rigidbody2D rb; // Reference to the Rigidbody2D component
    
    public float maxSpeed = 10f; // Maximum speed of the ball


    private void Start()
    {
        // Ensure startPos is set to the initial position of the ball
        if (startPos == Vector3.zero)
        {
            startPos = transform.position;
        }

        rb = GetComponent<Rigidbody2D>(); // Get the Rigidbody2D component
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
    }

    private void FixedUpdate()
    {
        // Clamp the ball's speed to the maximum speed
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag; // Get the tag of the collided object
        if (tag == "Brick")
        {
            if (brickBreakParticle != null) // Check if brickBreakParticle is assigned
            {
                GameObject particle = Instantiate(brickBreakParticle, collision.transform.position, Quaternion.identity); // Instantiate the particle effect
                Destroy(particle, 1f); // Destroy the particle effect after 1 second
            }

            Destroy(collision.gameObject); // Destroy the brick on collision
            PlayBrickBreakSound(); // Play the brick break sound

            if (SceneManager.GetActiveScene().name == "Pinball_1")
            {
                SceneController_Pinball_1 sceneController = Object.FindFirstObjectByType<SceneController_Pinball_1>();
                if (sceneController != null)
                {
                    sceneController.BrickDestroyed(); // Notify the SceneController
                }
            }
            else if (SceneManager.GetActiveScene().name == "Pinball_2")
            {
                SceneController_Pinball_2 sceneController = Object.FindFirstObjectByType<SceneController_Pinball_2>();
                if (sceneController != null)
                {
                    sceneController.BrickDestroyed(); // Notify the SceneController
                }
            }
        }
        else if (tag == "Bumper")
        {
            PlayBumperSound(); // Play the bumper sound
        }
        else if (tag == "Dead")
        {
            StartCoroutine(RespawnBall());  
            PlayDeathSound(); // Play the death sound
        }
    }

    private void PlayDeathSound()
    {
        if (audioSource != null && deathSound != null) // Check if audioSource is not set and deathSound is assigned
        {
            audioSource.PlayOneShot(deathSound); // Play the death sound
        }
    }

    private void PlayBrickBreakSound()
    {
        if (audioSource != null && brickBreakSound != null) // Check if audioSource is not set and brickBreakSound is assigned
        {
            audioSource.PlayOneShot(brickBreakSound); // Play the brick break sound
        }
    }

    private void PlayBumperSound()
    {
        if (audioSource != null && bumberSound != null) // Check if audioSource is not set and bumberSound is assigned
        {
            audioSource.PlayOneShot(bumberSound); // Play the bumper sound
        }
    }

    private System.Collections.IEnumerator RespawnBall()
    {
        rb.linearVelocity = Vector2.zero; // Stop the ball's movement
        rb.bodyType = RigidbodyType2D.Kinematic; // Disable physics interactions
        transform.position = startPos; // Move the ball to the starting position

        yield return new WaitForSeconds(2f); // Wait for 2 seconds

        rb.bodyType = RigidbodyType2D.Dynamic; // Re-enable physics interactions
    }
}
