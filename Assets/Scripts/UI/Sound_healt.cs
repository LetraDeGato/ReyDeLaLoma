using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioReproducer : MonoBehaviour
{
    public AudioSource audioSource; // Asigna este componente desde el editor

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Asegúrate de que el jugador tenga un tag "Player"
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogError("No se encontró el componente AudioSource.");
        }
    }
}