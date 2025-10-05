using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class RigidbodyMovement : MonoBehaviour
{
    [Header("Speeds (m/s)")]
    public float walkSpeed = 2.5f;
    public float sprintSpeed = 5.5f;
    public float crouchSpeed = 1f;

    float moveSpeed;

    [Header("Crouch Settings")]
    public float crouchHeight = 0.5f;
    public float crouchSmoothTime = 0.15f;
    public LayerMask groundMask;

    [Header("References")]
    public Rigidbody rb;
    public CapsuleCollider capsuleCollider;
    public Transform cameraTransform;
    public float cameraOffset = 0.5f;
    public float crouchCameraOffset = 0.1f;

    private bool isCrouching;

    void Start()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (capsuleCollider == null) capsuleCollider = GetComponent<CapsuleCollider>();
        if (cameraTransform == null) cameraTransform = Camera.main.transform;

        // Lock rotation on X and Z
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        HandleCrouch();
        HandleMovement();
    }

    void HandleMovement()
    {
        Vector2 input = InputManager.Move; // X = horizontal, Y = vertical

        // Determine speed
        isCrouching = InputManager.Crouch;
        bool isSprinting = InputManager.Run && !isCrouching;

        // Calculate movement
        Vector3 move = (transform.right * input.x + transform.forward * input.y).normalized * moveSpeed;

        // Move Rigidbody
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }

    void HandleCrouch()
    {
        float checkRadius = capsuleCollider.radius * 0.9f;
        float checkHeight = 0.5f;
        Vector3 checkPos = transform.position + Vector3.up * checkHeight;

        bool obstacleAbove = Physics.CheckSphere(checkPos, checkRadius, groundMask, QueryTriggerInteraction.Ignore);

        // État de crouch basé sur input + obstacle
        if (InputManager.Crouch && !isCrouching)
        {
            isCrouching = true;
        }
        else if (!InputManager.Crouch && !obstacleAbove)
        {
            isCrouching = false;
        }

        // Si obstacle => forcer le crouch
        if (obstacleAbove)
            isCrouching = true;

        // Appliquer l’échelle et maintenir au sol
        transform.localScale = new Vector3(1, isCrouching ? crouchHeight : 1, 1);
        if (isCrouching)
            rb.AddForce(Vector3.down * 50f, ForceMode.Acceleration);

        // Déplacer la caméra
        cameraTransform.GetComponent<BodycamVHSController>().offset =
            new Vector3(0f, isCrouching ? crouchCameraOffset : cameraOffset, 0f);

        // Gérer la vitesse ici
        if (isCrouching)
        {
            moveSpeed = crouchSpeed;
        }
        else if (InputManager.Run)
        {
            moveSpeed = sprintSpeed;
        }
        else
        {
            moveSpeed = walkSpeed;
        }
    }

    private void OnDrawGizmos()
    {
        if (capsuleCollider == null) return;

        float standingHeight = 0.5f;
        float radius = capsuleCollider.radius * 0.9f;
        Vector3 checkPos = transform.position + Vector3.up * standingHeight;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(checkPos, radius);
    }
}