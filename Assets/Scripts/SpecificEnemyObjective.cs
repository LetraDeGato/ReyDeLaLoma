using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecificEnemyObjective : MonoBehaviour
{
    [Header("Enemigo Específico")]
    [Tooltip("x")]
    public GameObject targetEnemy;

    [Header("Elementos a Activar")]
    [Tooltip(".")]
    public GameObject victoryCanvas; 
    [Tooltip(" objeto activará ")]
    public GameObject objectToActivate; 
    public float displayDuration = 3f;

    private bool objectiveCompleted = false;

    void Start()
    {
        //n ocultos/desactivados
        if (victoryCanvas != null) victoryCanvas.SetActive(false);
        if (objectToActivate != null) objectToActivate.SetActive(false);
    }

    void Update()
    {
        if (objectiveCompleted) return;

        // su referencia pasa a ser 'null'
        if (targetEnemy == null)
        {
            TriggerRewardSequence();
        }
    }

    void TriggerRewardSequence()
    {
        objectiveCompleted = true;
        Debug.Log("¡!");

        // 1. Desplegar el Canvas
        if (victoryCanvas != null)
        {
            victoryCanvas.SetActive(true);
        }

        // 2. Activar el objeto posterior
        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }
    }
    IEnumerator HideCanvasAfterSeconds()
    {
        // Espera
        yield return new WaitForSeconds(displayDuration);

        // Oculta el letrero
        victoryCanvas.SetActive(false);
        Debug.Log(" desaparecido.");
    }
}
