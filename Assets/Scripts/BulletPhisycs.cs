using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhisycs : MonoBehaviour
{
    public float speed = 30f;
    public int damage = 1;
    public float lifetime = 3f;
    private bool isDestroyed = false;

    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime);

        direction = transform.forward;
    }
    void Update()
    {
        // Mueve la bala en la dirección establecida
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1); // Asegúrate de pasar 1 de daño
                Debug.Log("Disparo impactó al enemigo");
            }
            Destroy(gameObject); // Destruye la bala
        }
    }
   // cambio
    
}
