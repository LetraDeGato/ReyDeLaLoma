using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Configuración de Disparo")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 25f;
    public float destroyTime = 3f;

    [Tooltip("Capa del Player para evitar que el raycast choque con su propio cuerpo")]
    public LayerMask ignoreLayers;

    [Header("Apuntado (Cámaras Cinemachine)")]
    [Tooltip("Arrastra aquí tu VCam de exploración (Normal)")]
    public CinemachineVirtualCamera exploreVCam;

    [Tooltip("Arrastra aquí tu VCam de apuntado (Zoom)")]
    public CinemachineVirtualCamera aimVCam;

    private Camera mainCam;

    private void Start()
    {
        // Detecta automáticamente la Main Camera del juego
        if (Camera.main != null)
        {
            mainCam = Camera.main;
        }
        else
        {
            Debug.LogError("PlayerShooting: No se encontró la Main Camera. Asegúrate de que tenga el Tag 'MainCamera'.");
        }
    }

    private void Update()
    {
        HandleAiming();

        if (Input.GetMouseButtonDown(0)) // Clic izquierdo para disparar
        {
            Shoot();
        }
    }

    private void HandleAiming()
    {
        // Clic derecho presionado = Apuntando
        bool isAiming = Input.GetMouseButton(1);

        // Si asignaste ambas cámaras en el Inspector, alterna la prioridad
        if (exploreVCam != null && aimVCam != null)
        {
            if (isAiming)
            {
                aimVCam.Priority = 20;
                exploreVCam.Priority = 10;
            }
            else
            {
                aimVCam.Priority = 10;
                exploreVCam.Priority = 20;
            }
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;
        if (mainCam == null) mainCam = Camera.main;
        if (mainCam == null) return;

        // Trazado de rayo desde el centro exacto de la pantalla (0.5, 0.5)
        Ray ray = mainCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 targetPoint;

        if (Physics.Raycast(ray, out RaycastHit hit, 1000f, ~ignoreLayers))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000f);
        }

        // Dirección de la bala desde la punta del arma hacia el punto objetivo
        Vector3 shootDirection = (targetPoint - firePoint.position).normalized;
        if (shootDirection == Vector3.zero) shootDirection = transform.forward;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(shootDirection));

        if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            rb.velocity = shootDirection * bulletSpeed;
        }

        Destroy(bullet, destroyTime);
    }
}