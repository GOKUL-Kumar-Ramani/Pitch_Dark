using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 5f;
    public float turnSpeed = 720f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public AudioClip WalkSound;
    private AudioSource audioSource;

    private float verticalVelocity;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (!audioSource)
            audioSource = gameObject.AddComponent<AudioSource>();
    }
    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        float moveVertical = 0f;
        float moveHorizontal = 0f;

        // Desktop keyboard input
        moveVertical = Input.GetAxis("Vertical");
        moveHorizontal = Input.GetAxis("Horizontal");

        // Mouse movement input (left/right)
        //if (Input.GetMouseButton(0)) // Only when left mouse is held
        //{
        //    float mouseDeltaX = Input.GetAxis("Mouse X");
        //    if (Mathf.Abs(mouseDeltaX) > 0.1f)
        //    {
        //        moveHorizontal = mouseDeltaX > 0 ? 1 : -1;
        //    }
        //}
        // Mouse movement input (left/right) without clicking
        float mouseDeltaX = Input.GetAxis("Mouse X");
        if (Mathf.Abs(mouseDeltaX) > 0.1f) // threshold to avoid noise
        {
            moveHorizontal = mouseDeltaX > 0 ? 1 : -1;
        }


        // Mobile touch input
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved)
            {
                moveHorizontal = touch.deltaPosition.x > 0 ? 1 : -1;
                moveVertical = touch.deltaPosition.y > 0 ? 1 : -1;
            }
        }

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 moveDirection = (forward * moveVertical + right * moveHorizontal).normalized;

        controller.Move(moveDirection * speed * Time.deltaTime);

        if (moveHorizontal != 0)
        {
            
            float rotationZ = moveHorizontal * turnSpeed * Time.deltaTime;
            transform.Rotate(0, 0, rotationZ);
        }
        if (moveVertical != 0)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("jump");
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        verticalVelocity += gravity * Time.deltaTime;
        velocity.y = verticalVelocity;

        controller.Move(velocity * Time.deltaTime);
    }

}
