using UnityEngine;

public class HorizontalCameraOrbit : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform playerTarget;

    [Header("Camera Position")]
    [SerializeField] private float distanceFromPlayer = 6.0f;
    [SerializeField] private float cameraHeight = 3.0f;

    [Header("Camera Rotation")]
    [SerializeField] private float horizontalSensitivity = 150.0f;

    private float horizontalAngle;

    private void Start()
    {
        Vector3 startingOffset = transform.position - playerTarget.position;
        horizontalAngle = Mathf.Atan2(
            startingOffset.x,
            startingOffset.z
        ) * Mathf.Rad2Deg;
    }

    private void LateUpdate()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        float mouseInput = Input.GetAxis("Mouse X");

        horizontalAngle +=
            mouseInput * horizontalSensitivity * Time.deltaTime;

        Quaternion horizontalRotation =
            Quaternion.Euler(0.0f, horizontalAngle, 0.0f);

        Vector3 cameraOffset = horizontalRotation *
            new Vector3(0.0f, cameraHeight, -distanceFromPlayer);

        transform.position = playerTarget.position + cameraOffset;

        Vector3 lookPosition =
            playerTarget.position + Vector3.up * cameraHeight * 0.5f;

        transform.LookAt(lookPosition);
    }
}