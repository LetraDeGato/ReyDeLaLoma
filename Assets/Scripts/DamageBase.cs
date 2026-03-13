using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBase : MonoBehaviour
{
    [SerializeField] private int dañoPortoque;

    void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Stats stats))
        {
            stats.TakeDamage(dañoPortoque);
        }
    }
}
