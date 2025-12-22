using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private LayerMask groundLayer;


    // Movement
    public float speed = 5f;
    public float jumpForce = 5f;

    // Mouse look
    public float mouseSensitivity = 200f;
    public float maxLookAngle = 80f;

    private Rigidbody rb;
    private float cameraRotation = 0f;
    private bool isGrounded;
    private bool jumpTriggered;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        cameraRotation -= mouseY;
        cameraRotation = Mathf.Clamp(cameraRotation, -maxLookAngle, maxLookAngle);
        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0f, 0f);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpTriggered = true;
        }
    }

    void FixedUpdate()
    {
        isGrounded = Physics.CheckSphere(transform.position + Vector3.down * 0.1f,0.2f,groundLayer);

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = (transform.right * x + transform.forward * z) * speed;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

        if (jumpTriggered)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumpTriggered = false;
        }
    }
}