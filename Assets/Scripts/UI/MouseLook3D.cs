using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook3D : MonoBehaviour
{
    public float rotationSpeed = 25f; 
    public bool instantRotation = false;

    private Transform mainCamTransform;

    private void Start()
    {
        if (Camera.main != null)
        {
            mainCamTransform = Camera.main.transform;
        }
    }

    private void Update()
    {
        if (mainCamTransform == null)
        {
            if (Camera.main != null) mainCamTransform = Camera.main.transform;
            else return;
        }

        // Obtiene la orientación horizontal de la cámara principal
        Vector3 cameraForward = mainCamTransform.forward;
        cameraForward.y = 0; 

        if (cameraForward.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);

            if (instantRotation)
            {
                transform.rotation = targetRotation;
            }
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
}