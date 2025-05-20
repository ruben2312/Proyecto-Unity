using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject ballPrefab; // Prefab for the ball
    [SerializeField]
    Vector3 startPos; // Starting position for the ball
    [SerializeField]
    private HingeJoint2D leftFlipper, rightFlipper;
    public AudioClip flipperSound;
    private AudioSource audioSource; // Reference to the AudioSource component
    public static GameManager instance; // Singleton instance of GameManager

    private void Awake()
    {
      if (instance == null)
      {
          instance = this; // Set the singleton instance
      }
      else
      {
          Destroy(gameObject); // Destroy duplicate instance
      } 


    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Instantiate(ballPrefab, startPos, Quaternion.identity); // Instantiate the ball at the starting position
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component}
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            PlayFlipperSound(); // Play the flipper sound
            JointMotor2D motor = leftFlipper.motor;
            motor.motorSpeed = -1000; // Speed of rotation
            leftFlipper.motor = motor;
            leftFlipper.useMotor = true;
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            JointMotor2D motor = leftFlipper.motor;
            motor.motorSpeed = 1000f; // Return to resting position
            leftFlipper.motor = motor;
            leftFlipper.useMotor = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayFlipperSound(); // Play the flipper sound
            JointMotor2D motor = rightFlipper.motor;
            motor.motorSpeed = 1000f; // Speed of rotation
            rightFlipper.motor = motor;
            rightFlipper.useMotor = true;
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            JointMotor2D motor = rightFlipper.motor;
            motor.motorSpeed = -1000f; // Return to resting position
            rightFlipper.motor = motor;
            rightFlipper.useMotor = true;
        }
    }

    private void PlayFlipperSound()
    {
        if (audioSource != null && flipperSound != null) // Check if audioSource is not set and flipperSound is assigned
        {
            audioSource.PlayOneShot(flipperSound); // Play the flipper sound
        }
    }
}
