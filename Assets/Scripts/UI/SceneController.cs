using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void CargarNivel(string nombreNivel)
    {
        StartCoroutine(CargarJuegoCorrutina(nombreNivel));
    }

    private IEnumerator CargarJuegoCorrutina(string nombreNivel)
    {
        // (limpia la escena anterior)
        AsyncOperation cargaNivel = SceneManager.LoadSceneAsync(nombreNivel, LoadSceneMode.Single);
        while (!cargaNivel.isDone) yield return null;

        // UI encima sin borrar el nivel
        AsyncOperation cargaUI = SceneManager.LoadSceneAsync("Escena_UI", LoadSceneMode.Additive);
        while (!cargaUI.isDone) yield return null;
    }
}
