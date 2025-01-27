using System;
using DG.Tweening;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public float speed = 2f; // Velocidad básica del pez
    protected Vector2 direction;
    public float verticalFrequency = 2f;
    public float verticalAmplitude = .2f;

    // Inicializa el pez con dirección y velocidad
    public virtual void Initialize(Vector2 spawnPosition, Vector2 moveDirection)
    {
        transform.position = spawnPosition;
        direction = moveDirection.normalized;
       
    }

    // Movimiento básico
    protected virtual void Update()
    {
        transform.Translate(direction * (speed * Time.deltaTime));
        // Agregar ruido de movimiento vertical
        float verticalOffset = Mathf.Sin(Time.time * verticalFrequency) * verticalAmplitude;
        transform.position = new Vector3(transform.position.x, transform.position.y + verticalOffset * Time.deltaTime, transform.position.z);

    }
}