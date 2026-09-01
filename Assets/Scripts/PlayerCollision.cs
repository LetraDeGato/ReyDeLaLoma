using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other)
    {
        PlaySound();
    }

    // Update is called once per frame
    void PlaySound()
    {
        // Aquí puedes agregar tu código para reproducir el sonido
        Debug.Log("Sonido reproducido!");
    }
}
