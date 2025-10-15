using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinningPlatform : MonoBehaviour
{
    public float rotationSpeed = 90f; // degrees per second

    void Update()
    {
        // Rotate around Z axis at rotationSpeed degrees per second
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}