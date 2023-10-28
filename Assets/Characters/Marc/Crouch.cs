using UnityEngine;

public class Crouch : MonoBehaviour
{
    [Header("Crouch Character Controller Varaibles")]
    [SerializeField] private float controllerHeightCrouch = 1.3f;
    [SerializeField] private float controllerCenterYCrouch = -0.15f;

    private bool isCrouching = false;

    private float controllerHeightDefault = 0.0f;
    private float controllerCenterYDefault = 0.0f;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        controllerHeightDefault = controller.height;
        controllerCenterYDefault = controller.center.y;
    }

    private void Update()
    {
        if (Input_Manager._INPUT_MANAGER.GetLeftShoulderValue() && controller.isGrounded)
        {
            isCrouching = !isCrouching;

            // Si estamos agachados, cambiaremos el tamaño del Character Controller
            // Esto se ha revisado en el inspector, aunque los valores se pueden settear
            // desde ahí
            //
            // La velocidad será modificada desde el Script de Movement, que se encarga
            // de gestionar el movimiento. Haremos un Getter para la variable isCrouching
            // y el Script se encargará de gestionarlo
            //
            // La animación de agacharse o levantarse sí que será gestionada en este Script, mientras que la de caminar estando agachado será hará en Movement

            if (isCrouching)
            {
                controller.center = new Vector3(controller.center.x, controllerCenterYCrouch, controller.center.z);
                controller.height = controllerHeightCrouch;
            }
            else
            {
                controller.center = new Vector3(controller.center.x, controllerCenterYDefault, controller.center.z);
                controller.height = controllerHeightDefault;
            }
        }
    }

    public bool GetIsCrouching() => isCrouching;


}
