using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed = 5.0f;
    [SerializeField] private float rotationSpeed = 10.0f;

    [Header("Jump")]
    [SerializeField] private float jumpHeight = 3.0f;
    [SerializeField] private float gravity = -9.81f;

    [Header("Direction Reference")]
    [SerializeField] private Transform cameraTransform;

    private CharacterController characterController;
    private float verticalVelocity;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (cameraTransform == null)
        {
            Debug.LogError(
                "PlayerMovement cannot find the Main Camera.",
                gameObject
            );

            return;
        }

        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0.0f;
        cameraRight.y = 0.0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 movementDirection =
            (cameraForward * verticalInput) +
            (cameraRight * horizontalInput);

        if (movementDirection.sqrMagnitude > 1.0f)
        {
            movementDirection.Normalize();
        }

        RotatePlayer(movementDirection);
        HandleJumpAndGravity();

        Vector3 finalMovement =
            (movementDirection * movementSpeed) +
            (Vector3.up * verticalVelocity);

        characterController.Move(finalMovement * Time.deltaTime);
    }

    private void RotatePlayer(Vector3 movementDirection)
    {
        if (movementDirection.sqrMagnitude <= 0.0f)
        {
            return;
        }

        Quaternion targetRotation =
            Quaternion.LookRotation(movementDirection, Vector3.up);

        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            targetRotation,
            rotationSpeed * Time.deltaTime
        );
    }

    private void HandleJumpAndGravity()
    {
        if (characterController.isGrounded && verticalVelocity < 0.0f)
        {
            verticalVelocity = -2.0f;
        }

        if (Input.GetKeyDown(KeyCode.Space) &&
            characterController.isGrounded)
        {
            verticalVelocity = Mathf.Sqrt(
                jumpHeight * -2.0f * gravity
            );
        }

        verticalVelocity += gravity * Time.deltaTime;
    }
}