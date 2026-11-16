using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Estadísticas del Enemigo")]
    public float maxHealth = 100f;
    private float currentHealth;
    public float moveSpeed = 4.5f;
    public float damage = 10f;

    [Header("Objetivo (Jugador)")]
    public Transform playerTransform;
    //public float stoppingDistance = 1.5f; 
    // Distancia a la que se detiene para atacar

    private NavMeshAgent agent;

    private void Start()
    {
        currentHealth = maxHealth;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = moveSpeed;
        //agent.stoppingDistance = stoppingDistance;

        // Busca automáticamente al jugador "Tag" 
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
            else
            {
                Debug.LogWarning("No se encontró ningún objeto con la etiqueta 'Player' en la escena.");
            }
        }
    }

    private void Update()
    {
        // NavMesh hacia  posición
        if (playerTransform == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                playerTransform = playerObj.transform;
            }
            return;
        }
        if (agent != null)
    {
        agent.SetDestination(playerTransform.position);
    }
}

    // Método para recibir daño (puedes llamarlo desde tus proyectiles o disparos)
    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log(" derrotado!");
        // Aquí puedes añadir sumar puntos, etc.
        Destroy(gameObject);
    }
}
    

