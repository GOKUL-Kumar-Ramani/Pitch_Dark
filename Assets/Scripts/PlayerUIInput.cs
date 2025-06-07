using UnityEngine;

public class PlayerUIInput : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float turnSpeed = 120f; // rotation speed in degrees per second
    public AudioClip WalkSound;

    private AudioSource audioSource;

    // Input flags
    private bool moveUp, moveDown, moveLeft, moveRight;

    void Start()
    {
        // Set up audio source
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        float moveVertical = 0f;

        // Forward/Backward movement
        if (moveUp) moveVertical = 1f;
        if (moveDown) moveVertical = -1f;

        // Get camera's forward direction
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        // Move the player
        Vector3 moveDirection = forward * moveVertical;
        controller.Move(moveDirection * speed * Time.deltaTime);

        // Left/Right rotation
        float rotationInput = 0f;
        if (moveLeft) rotationInput = -1f;
        if (moveRight) rotationInput = 1f;

        if (rotationInput != 0f)
        {
            transform.Rotate(0f, rotationInput * turnSpeed * Time.deltaTime, 0f);
        }

        // Walking sound
        if (moveVertical != 0f || rotationInput != 0f)
        {
            if (WalkSound && !audioSource.isPlaying)
            {
                audioSource.clip = WalkSound;
                audioSource.loop = true;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
                audioSource.Stop();
        }
    }

    // UI button press/release handlers
    public void OnPressUp() => moveUp = true;
    public void OnReleaseUp() => moveUp = false;

    public void OnPressDown() => moveDown = true;
    public void OnReleaseDown() => moveDown = false;

    public void OnPressLeft() => moveLeft = true;
    public void OnReleaseLeft() => moveLeft = false;

    public void OnPressRight() => moveRight = true;
    public void OnReleaseRight() => moveRight = false;
}
