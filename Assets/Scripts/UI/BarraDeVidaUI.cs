using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraDeVidaUI : MonoBehaviour
{
    [SerializeField] private Slider sliderBarraDeVida;
    [SerializeField] private Stats vidaJugador;



    private void Start()
    {
        vidaJugador = FindFirstObjectByType<Stats>();

        vidaJugador.PlayerTakeDmg += CambiarBarraDeVida;

        IniciarBarraDeVida(vidaJugador.GetVidaMaxima(), vidaJugador.GetVidaActual());
    }

    void OnDisable()
    {
        vidaJugador.PlayerTakeDmg -= CambiarBarraDeVida;
    }

    private void IniciarBarraDeVida(int vidaMax, int vidaActual) 
    {
        sliderBarraDeVida.maxValue = vidaMax;
        sliderBarraDeVida.value = vidaActual;
    }
    private void CambiarBarraDeVida(int vidaActual)
    {
        sliderBarraDeVida.value = vidaActual;
    }

    
}

    
