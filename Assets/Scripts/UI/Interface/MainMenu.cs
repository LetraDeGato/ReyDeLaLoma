using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Paneles de la UI")]
    public GameObject mainMenuPanel;    // Panel principal con los botones Jugar, Opciones, Salir
    public GameObject optionsPanel;     

    [Header("Nombre de la Escena de Juego")]
    public string gameSceneName = "Level_01"; 

    private void Start()
    {
        ShowMainMenu();
    }


    public void PlayGame()
    {
        Debug.Log("Cargando nivel: " + gameSceneName);
        SceneManager.LoadScene(gameSceneName);
    }

    public void OpenOptions()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(false);
        if (optionsPanel != null) optionsPanel.SetActive(true);
    }

    
    public void ShowMainMenu()
    {
        if (mainMenuPanel != null) mainMenuPanel.SetActive(true);
        if (optionsPanel != null) optionsPanel.SetActive(false);
    }

  
    public void QuitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}