using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private PlayerInputActions playerInputs;

    private Vector2 leftAxisValue = Vector2.zero;
    private Vector2 rightAxisValue = Vector2.zero;
    private bool southButton = false;
    private bool leftShoulder = false;

    private float southButtonTimer = 0.0f;
    private float leftShoulderTimer = 0.0f;

    private void Awake()
    {
        if (_INPUT_MANAGER != null && _INPUT_MANAGER != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            // Generamos la instancia y activamos el Character Scheme
            playerInputs = new PlayerInputActions();
            playerInputs.Gameplay.Enable();

            // Delegates
            playerInputs.Gameplay.Move.performed += LeftAxisUpdate;
            playerInputs.Gameplay.RotateCamera.performed += RightAxisUpdate;
            playerInputs.Gameplay.Jump.performed += SouthButtonUpdate;
            playerInputs.Gameplay.Crouch.performed += LeftShoulderUpdate;

            _INPUT_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        // Pasamos los booleanos a false y resetear el valor de estos
        this.southButton = false;
        this.leftShoulder = false;

        this.southButtonTimer += Time.deltaTime;
        this.leftShoulderTimer += Time.deltaTime;

        InputSystem.Update();
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        this.leftAxisValue = context.ReadValue<Vector2>();
    }

    private void RightAxisUpdate(InputAction.CallbackContext context)
    {
        this.rightAxisValue = context.ReadValue<Vector2>();
    }

    private void SouthButtonUpdate(InputAction.CallbackContext context)
    {
        this.southButton = true;
        this.southButtonTimer = 0.0f;
    }

    private void LeftShoulderUpdate(InputAction.CallbackContext context)
    {
        this.leftShoulder = true;
    }

    public Vector2 GetLeftAxisValue() => this.leftAxisValue;
    public Vector2 GetRightAxisValue() => this.rightAxisValue;
    public bool GetButtonSouthValue() => this.southButton;
    public float GetButtonSouthTimer() => this.southButtonTimer;
    public bool GetLeftShoulderValue() => this.leftShoulder;
    public float GetLeftShoulderTimer() => this.leftShoulderTimer;
}