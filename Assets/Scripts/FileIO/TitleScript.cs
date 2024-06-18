using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScript : MonoBehaviour
{
    public float scaleSpeed = 0.5f; 
    public float scaleAmount = 0.1f; 
    public float rotationSpeed = 50f; 
    public float rotationAmount = 10f; 

    private Vector3 initialScale;
    private Quaternion initialRotation;

    void Start()
    {
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        float scaleMultiplier = 1.0f + Mathf.Sin(Time.time * scaleSpeed) * scaleAmount;
        transform.localScale = initialScale * scaleMultiplier;

        float rotationMultiplier = Mathf.Sin(Time.time * scaleSpeed) * rotationAmount;
        Quaternion targetRotation = initialRotation * Quaternion.Euler(0f, 0f, rotationMultiplier);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
