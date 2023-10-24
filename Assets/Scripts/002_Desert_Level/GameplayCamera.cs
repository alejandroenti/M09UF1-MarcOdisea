using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayCamera : MonoBehaviour
{
    [Header("Camera Movement Configuration")]
    [SerializeField] private GameObject target;
    [SerializeField] private float targetDistance;
    [SerializeField] private float cameraLerp;

    private float rotationX;
    private float rotationY;

    private void LateUpdate()
    {
        // Comprombamos que haya rotación almacenada en el Right Axis
        //  En caso de tener, sumamos rotación
        //  En caso contrario, resetearemos la rotación a 0 para que pare de rotar

        // ROTATION X
        if (Input_Manager._INPUT_MANAGER.GetRightAxisValue().y != 0.0f)
        {
            rotationX += Input_Manager._INPUT_MANAGER.GetRightAxisValue().y;
        }

        // ROTATION Y
        if (Input_Manager._INPUT_MANAGER.GetRightAxisValue().x != 0.0f)
        {
            rotationY += Input_Manager._INPUT_MANAGER.GetRightAxisValue().x;
        }

        // Limitamos la rotación en el eje Y
        rotationX = Mathf.Clamp(rotationX, -50f, 50f);

        // Rotamos la cámara para después trasladarla
        transform.eulerAngles = new Vector3(rotationX, rotationY, 0);

        Vector3 finalPosition = Vector3.Lerp(transform.position, target.transform.position - transform.forward * targetDistance, cameraLerp * Time.deltaTime);

        // Realizamos un Linecast para detectar si algo se interpone entre el jugador y la cámara
        // Se hace desde el jugador para que el hit (donde trasladeremos la cámara) sea por delante
        // de la pared y podamos continuar viendo al jugador
        RaycastHit hit;

        if (Physics.Linecast(target.transform.position, finalPosition, out hit))
        {
            finalPosition = hit.point;
            Debug.Log("Entro");
        }
            

        transform.position = finalPosition;
    }
}
