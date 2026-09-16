using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPSCamera3D : MonoBehaviour
{
    [Header("Objetivo")]
    public Transform player;

    [Header("Sensibilidad")]
    public float mouseSensitivity = 3f;
    public float minPitch = -35f;
    public float maxPitch = 60f;

    [Header("Posición de Hombro (Offset)")]
    public Vector3 offset = new Vector3(0.8f, 1.8f, -3.5f);

    private float yaw;
    private float pitch;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Cargar rotación inicial de la cámara para evitar saltos repentinos al arrancar
        Vector3 currentAngles = transform.eulerAngles;
        pitch = currentAngles.x;
        yaw = currentAngles.y;
    }

    private void LateUpdate()
    {
        if (player == null) return;

        // GetAxisRaw lee directamente la velocidad de movimiento del ratón sin suavizado previo
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        yaw += mouseX * mouseSensitivity;
        pitch -= mouseY * mouseSensitivity;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Quaternion cameraRotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 targetPosition = player.position + cameraRotation * offset;

        transform.rotation = cameraRotation;
        transform.position = targetPosition;
    }
}