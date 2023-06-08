using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Rigidbody2D component of the player
    private Rigidbody2D rb;

    // Animator component of the player
    private Animator animator;

    // AudioSource component of the player
    private AudioSource audioSource;

    // Boolean values to store if the player is grounded or has already performed a double jump
    private bool isGrounded;
    private bool canDoubleJump;

    // The audio clips
    public AudioClip jumpSound;
    public AudioClip runningSound;
    public AudioClip dyingSound;

    private Health playerHealth; //Making a reference to the health script



    public AudioMixerGroup sfxMixerGroup; // for sound effects

    // Constant values for move speed and jump force
    public float moveSpeed = 5f;
    public float jumpForce = 5f;

    // to keep note of the players starting position 
    private Vector2 initialPosition;

    void Start()
    {
        // Getting the Rigidbody2D, Animator, and AudioSource components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        playerHealth = GetComponent<Health>();

        // Add AudioSource if it doesn't exist
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Assign the output AudioMixerGroup of the AudioSource to the SFX group
        audioSource.outputAudioMixerGroup = sfxMixerGroup;

        // Freezing the rotation of the player to avoid unwanted rotation
        rb.freezeRotation = true;

        initialPosition = transform.position;
    }


    void Update()
    {
        // Moving the player and flipping the sprite based on input
        MovePlayer();
        Flip();

        // If the Jump button is pressed and the player is either grounded or can perform a double jump, jump
        if (Input.GetButtonDown("Jump") && (isGrounded || canDoubleJump))
        {
            Jump();
        }
    }

    void MovePlayer()
    {
        // Getting the horizontal input
        float move = Input.GetAxis("Horizontal");

        // Applying the movement to the player
        rb.velocity = new Vector2(move * moveSpeed, rb.velocity.y);

        // If the player is moving, set the isRunning animation parameter to true
        if (!Mathf.Approximately(move, 0))
        {
            animator.SetBool("isRunning", true);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(runningSound);
            }
        }
        // Otherwise, set it to false
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    void Jump()
    {
        // If the player is grounded, they can perform a double jump in the future
        if (isGrounded)
        {
            canDoubleJump = true;
        }
        // If the player is not grounded but can perform a double jump, they cannot perform another one in the future
        else if (canDoubleJump)
        {
            canDoubleJump = false;
        }

        // Jump by setting the vertical velocity to the jump force
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);

        // Set the isJumping animation parameter to true
        animator.SetBool("isJumping", true);
        audioSource.PlayOneShot(jumpSound);
    }

    void Flip()
    {
        // Flipping the sprite based on the horizontal input
        Vector3 characterScale = transform.localScale;
        if (Input.GetAxis("Horizontal") < 0)
        {
            characterScale.x = -1;
        }
        if (Input.GetAxis("Horizontal") > 0)
        {
            characterScale.x = 1;
        }
        transform.localScale = characterScale;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // If the player collides with the ground, set isGrounded to true and set the isJumping animation parameter to false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
        // If the player collides with water, trigger the Death animation and reset the player
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Water"))
        {
            StartCoroutine(Death());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // If the player stops colliding with the ground, set isGrounded to false
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

     IEnumerator Death()
    {
        animator.SetTrigger("Death");
        audioSource.PlayOneShot(dyingSound);

        // Wait for the current animation to finish
        while (animator.GetCurrentAnimatorStateInfo(0).IsName("Death") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            yield return null;
        }
     
    }


    public void ResetPlayer()
    {
        transform.position = initialPosition;
        rb.velocity = Vector2.zero;
        animator.Rebind();
        isGrounded = true;
        canDoubleJump = false;
        playerHealth.ResetHealth();
    }
}
