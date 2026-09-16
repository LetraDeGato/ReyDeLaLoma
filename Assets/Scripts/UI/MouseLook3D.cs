using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook3D : MonoBehaviour
{
    public float rotationSpeed = 25f; 
    public bool instantRotation = false;
    public Camera playerCamera; 

    private void Start()
    {
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    private void Update()
    {
        if (playerCamera == null) return;

        // Alinear 
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0; 

        if (cameraForward.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(cameraForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}