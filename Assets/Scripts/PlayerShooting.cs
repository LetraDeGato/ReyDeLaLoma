using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
       
        public GameObject bulletPrefab;
        public Transform firePoint;
        public Camera playerCamera;

        
        public float bulletSpeed = 25f;
        public float destroyTime = 3f;

        // inmunidad al jugador a sus propios disparos
        public LayerMask ignoreLayers;

        private void Start()
        {
            if (playerCamera == null) playerCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (bulletPrefab == null || firePoint == null || playerCamera == null) return;

            // (mirilla TPS)
            Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            Vector3 targetPoint;

            // ignorar al jugador
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, ~ignoreLayers))
            {
                targetPoint = hit.point;
            }
            else
            {
                targetPoint = ray.GetPoint(1000f);
            }

            //  Dirección 
            Vector3 shootDirection = (targetPoint - firePoint.position).normalized;

            // Instanciar y aplicar velocidad
            Quaternion bulletRotation = Quaternion.LookRotation(shootDirection);
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

            if (bullet.TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                rb.velocity = shootDirection * bulletSpeed;
            }

            Destroy(bullet, destroyTime);
        }
    }
