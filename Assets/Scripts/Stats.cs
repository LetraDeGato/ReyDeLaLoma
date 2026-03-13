using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public Action<int> PlayerTakeDmg;
    [SerializeField] private int vidaMaxima;
    [SerializeField] private int vidaActual;
   

    private void Awake()
    {
        vidaActual = vidaMaxima;
    }

    public void TakeDamage(int daño)
    {
        int vidaTemporal = vidaActual - daño;

        vidaTemporal = Mathf.Clamp(vidaTemporal, 0, vidaMaxima);

        vidaActual = vidaTemporal; 

        PlayerTakeDmg?.Invoke (vidaActual);

        if (vidaActual <= 0)
        {
            Muerte();
        }
    }

    private void Muerte()
    {
        Destroy(gameObject);

    }

    public int GetVidaMaxima() => vidaMaxima;

    public int GetVidaActual() => vidaActual;
}
