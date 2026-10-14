using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject pausePanel;
    
    public static bool isPaused = false;

    private void Start()
    {
        // Asegurar que el menú de pausa inicie oculto
        if (pausePanel != null) pausePanel.SetActive(false);
    }

    private void Update()
    {
        // Solo permite pausar con Escape/P si el tutorial ya no está en pantalla
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
    {
        if (pausePanel != null) pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        // Mostrar mouse para hacer clic en los botones
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void Resume()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        // Bloquear mouse para seguir jugando en 3D
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}