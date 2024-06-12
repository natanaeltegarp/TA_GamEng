using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AnimationStateController : MonoBehaviour
{
    Animator animator; // Reference to the Animator component
    NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component
    private float walkTimer; // Timer for walking
    private float runTimer; // Timer for running
    private bool isRunning; // Boolean to track the current state
    public float walkInterval = 7.0f; // Time interval for walking
    public float runInterval = 10.0f; // Time interval for running
    public float walkSpeed = 3.5f; // Speed for walking
    public float runSpeed = 7.0f; // Speed for running
    public Transform player; // Reference to the player
    public float detectionRange = 1.5f; // Range to detect the player and restart the game
    private AudioSource stepSound; // Reference to the AudioSource component for steps
    private AudioSource gruntSound; // Reference to the AudioSource component for grunts

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        AudioSource[] audioSources = GetComponents<AudioSource>(); // Get all AudioSource components
        stepSound = audioSources[0]; // Assuming the first one is for steps
        gruntSound = audioSources[1]; // Assuming the second one is for grunts

        walkTimer = walkInterval; // Initialize the walk timer
        runTimer = runInterval; // Initialize the run timer
        isRunning = false; // Start with walking state
        animator.SetBool("IsRunning", isRunning); // Ensure the initial state is walking
        navMeshAgent.speed = walkSpeed; // Set initial speed to walk speed
    }

    void Update()
    {
        if (!isRunning)
        {
            walkTimer -= Time.deltaTime; // Decrease the walk timer

            if (walkTimer <= 0)
            {
                isRunning = true; // Switch to running state
                animator.SetBool("IsRunning", isRunning); // Set the Animator parameter
                navMeshAgent.speed = runSpeed; // Change speed to run speed
                walkTimer = walkInterval; // Reset the walk timer

                // Play grunt sound when starting to run
                PlayGruntSound();
            }
        }
        else
        {
            runTimer -= Time.deltaTime; // Decrease the run timer

            if (runTimer <= 0)
            {
                isRunning = false; // Switch to walking state
                animator.SetBool("IsRunning", isRunning); // Set the Animator parameter
                navMeshAgent.speed = walkSpeed; // Change speed to walk speed
                runTimer = runInterval; // Reset the run timer
            }
        }

        // Chase the player
        navMeshAgent.SetDestination(player.position);

        // Play step sound if not already playing
        if (!stepSound.isPlaying)
        {
            stepSound.Play();
        }

        // Check if the enemy has reached the player
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            RestartGame();
        }
    }

    private void PlayGruntSound()
    {
        if (!gruntSound.isPlaying)
        {
            gruntSound.Play();
        }
    }

    private void RestartGame()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
