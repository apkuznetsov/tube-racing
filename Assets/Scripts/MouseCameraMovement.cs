using UnityEngine;

public class MouseCameraMovement : MonoBehaviour
{
    [SerializeField] private Transform mainCamera;

    [SerializeField] private float sensitivity = 10f;
    [SerializeField] private float maxYAngle = 80f;

    private Vector2 currentRotation;

    private void Update()
    {
        currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
        currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;

        currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
        currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

        mainCamera.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);

        if (Input.GetMouseButtonDown(0))
            Cursor.lockState = CursorLockMode.Locked;
    }
}