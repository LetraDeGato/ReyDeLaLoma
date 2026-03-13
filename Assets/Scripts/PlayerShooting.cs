using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform firePoint; // Objeto hijo del Player que marca el origen del disparo
    public float bulletSpeed = 15f;

    public Camera PlayerCamera; //la camara se va a arrastrar hasta aca

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Click izquierdo
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Ray ray = PlayerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 shootDirection;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            shootDirection = (hit.point - firePoint.position).normalized;
        }
        else
        {
            shootDirection = PlayerCamera.transform.forward;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody>().velocity = firePoint.forward * bulletSpeed;
        Destroy(bullet, 3f); // Destruye el proyectil después de 3 segundos
    }
}
