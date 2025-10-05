using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    PlayerInput PlayerInput;

    InputAction MoveAction;
    InputAction LookAction;
    InputAction RunAction;
    InputAction CrouchAction;

    public static Vector2 Move;
    public static Vector2 Look;
    public static bool Run;
    public static bool Crouch;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        PlayerInput = GetComponent<PlayerInput>();

        MoveAction = PlayerInput.actions["Move"];
        LookAction = PlayerInput.actions["Look"];
        RunAction = PlayerInput.actions["Sprint"];
        CrouchAction = PlayerInput.actions["Crouch"];
    }

    // Update is called once per frame
    void Update()
    {
        Move = MoveAction.ReadValue<Vector2>();
        Look = LookAction.ReadValue<Vector2>();

        Run = RunAction.IsPressed();
        Crouch = CrouchAction.IsPressed();
    }
}
