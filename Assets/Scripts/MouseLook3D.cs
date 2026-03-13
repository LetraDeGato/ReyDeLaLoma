using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook3D : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public Transform cameraTransform; // Arrastra la cámara principal aquí

    void Update()
    {
        // Obtener posición del mouse en el mundo 3D
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;

        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 targetPoint = ray.GetPoint(rayDistance);
            
            // Calcular dirección sin afectar la altura (Y)
            Vector3 direction = targetPoint - transform.position;
            direction.y = 0; 
            
            // Rotación suave hacia el punto
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(
                    transform.rotation, 
                    targetRotation, 
                    rotationSpeed * Time.deltaTime
                );
            }
        }
    }
}