using UnityEngine;
using UnityEngine.InputSystem;

public class Input_Manager : MonoBehaviour
{
    public static Input_Manager _INPUT_MANAGER;

    private PlayerInputActions playerInputs;

    private Vector2 leftAxisValue = Vector2.zero;
    private Vector2 rightAxisValue = Vector2.zero;

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

            _INPUT_MANAGER = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Update()
    {
        InputSystem.Update();
    }

    private void LeftAxisUpdate(InputAction.CallbackContext context)
    {
        this.leftAxisValue = context.ReadValue<Vector2>();
    }

    private void RightAxisUpdate(InputAction.CallbackContext context)
    {
        this.rightAxisValue = context.ReadValue<Vector2>();

//        Debug.Log("Getter Right Axis : " + this.rightAxisValue);
    }


    public Vector2 GetLeftAxisValue() => this.leftAxisValue;

    public Vector2 GetRightAxisValue()
    {
        Debug.Log("Getter Right Axis: " + this.rightAxisValue);
        return this.rightAxisValue;
    }
}