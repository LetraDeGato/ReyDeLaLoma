using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collaider_raduis : MonoBehaviour
{
    public Transform sphereCenter; 
    public float radius = 50f;     

    void Update()
    {
        if (sphereCenter == null) return;

        //  centro y el personaje
        Vector3 direction = transform.position - sphereCenter.position;
        float distance = direction.magnitude;

        // lo devuelve al límite
        if (distance > radius)
        {
            transform.position = sphereCenter.position + direction.normalized * radius;
        }
    }
}