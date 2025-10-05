using UnityEngine;

[RequireComponent(typeof(Camera))]
public class BodycamVHSController : MonoBehaviour
{
    [Header("Look Settings")]
    public float mouseSensitivity = 40f;
    public float clampAngle = 85f;

    private float rotationX = 0f;
    private float rotationY = 0f;

    [Header("Camera Settings")]
    public Transform playerTransform;
    public Vector3 offset = new Vector3(0f, 1.6f, 0f);

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 angles = transform.localEulerAngles;
        rotationX = angles.x;
        rotationY = angles.y;
    }

    void Update()
    {
        HandleMouseLook();
    }

    void LateUpdate()
    {
        if (playerTransform == null) return;

        // La caméra suit le joueur
        transform.position = playerTransform.position + offset;
    }

    private void HandleMouseLook()
    {
        float mouseX = InputManager.Look.x * mouseSensitivity * Time.deltaTime;
        float mouseY = InputManager.Look.y * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -clampAngle, clampAngle);
        rotationY += mouseX;

        transform.localRotation = Quaternion.Euler(rotationX, rotationY, 0f);
        playerTransform.rotation = Quaternion.Euler(0f, rotationY, 0f);
    }
}