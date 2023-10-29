using UnityEngine;

public class ThrowCappy : MonoBehaviour
{
    [Header("Cappy Element")]
    [SerializeField] private GameObject cappy;

    private bool canThrowCappy = true;
    private Vector3 spawnPosition = Vector3.zero;

    private CharacterController controller;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Si pulsamos el botón de Cappy, en este Script se gestiona el Spawn de este.
        // El comportamiento (Rotación, alejamiento y vuelta) se tratará en otro Script
        // El impulso al saltar sobre él se hara también en otro comportamiento

        if (Input_Manager._INPUT_MANAGER.GetRightShoulderValue() && canThrowCappy)
        {
            canThrowCappy = false;
            spawnPosition = transform.position;
            spawnPosition.x += controller.radius * 2;
            spawnPosition.y -= controller.radius;
            spawnPosition.z += controller.radius * 2;
            GameObject temp = Instantiate(cappy, spawnPosition, transform.rotation);
        }
    }

    public void SetCanThrowCappy(bool hasReturned) => this.canThrowCappy = hasReturned;
}
