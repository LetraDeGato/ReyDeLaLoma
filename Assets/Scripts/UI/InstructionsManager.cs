using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject tutoPanel;
    public Slider holdProgressBar;

    [Header("Configuración")]
    public float holdDuration = 1.5f; // Segundos que hay que mantener Space
    private float holdTimer = 0f;

    private void Start()
    {
        // Pausar el juego para que lean el tutorial al iniciar
        Time.timeScale = 0f;

        if (tutoPanel != null) tutoPanel.SetActive(true);
        if (holdProgressBar != null) holdProgressBar.value = 0f;
    }

    private void Update()
    {
        // Si el tutorial ya no está activo, no ejecutamos nada más
        if (tutoPanel == null || !tutoPanel.activeSelf) return;

        if (Input.GetKey(KeyCode.Space))
        {
            // Usamos unscaledDeltaTime porque Time.timeScale está en 0
            holdTimer += Time.unscaledDeltaTime;

            if (holdProgressBar != null)
            {
                holdProgressBar.value = holdTimer / holdDuration;
            }

            if (holdTimer >= holdDuration)
            {
                StartGame();
            }
        }
        else
        {
            holdTimer = 0f;
            if (holdProgressBar != null) holdProgressBar.value = 0f;
        }
    }

    private void StartGame()
    {
        Time.timeScale = 1f; // Inicia el tiempo del juego
        if (tutoPanel != null) tutoPanel.SetActive(false); // Oculta el panel
    }
}