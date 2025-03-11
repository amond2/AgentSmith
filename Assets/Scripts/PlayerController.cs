using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    
    [Header("Player Movement")]
    private Rigidbody rigidbody;
    private float moveSpeed = 5f;
    private float jumpForce = 7f;
    private bool isGrounded = false;
    
    [Header("Mouse Look Settings")]
    public Transform cameraTransform;
    private float mouseSensitivity = 2f;
    private float rotationX = 0f;


    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Move();
        MouseLook();
        if (Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded)
        {
            Debug.Log("Jump Pressed");
            Jump();
        }
    }

    private void Move()
    {
        float moveX = Keyboard.current.aKey.isPressed ? -1 : Keyboard.current.dKey.isPressed ? 1 : 0;
        float moveZ = Keyboard.current.sKey.isPressed ? -1 : Keyboard.current.wKey.isPressed ? 1 : 0;

        Vector3 moveDirection = cameraTransform.forward * moveZ + cameraTransform.right * moveX;
        moveDirection.y = 0f;

        rigidbody.velocity = new Vector3(moveDirection.x * moveSpeed, rigidbody.velocity.y, moveDirection.z * moveSpeed);
    }
    
    private void MouseLook()
    {
        float mouseX = Mouse.current.delta.x.ReadValue() * mouseSensitivity;
        float mouseY = Mouse.current.delta.y.ReadValue() * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0f, 0f);
    }

    private void Jump()
    {
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isGrounded = false;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }
    
}