using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int maxHealth = 5;
    private int currentHealth;

    private EnemySpawner spawner;

    void Start()
    {
        currentHealth = maxHealth;
        Debug.Log("Salud inicial del enemigo: " + currentHealth);

        spawner = FindAnyObjectByType<EnemySpawner>();
    }
    public void TakeDamage(int damage)

    {
        if (currentHealth <= 0) return;
        
        currentHealth -= damage;
        Debug.Log("Daño recibido. Salud restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Enemigo eliminado");

        Destroy(gameObject); // Destruye el enemigo cuando su salud llega a 0
    }
}
    

