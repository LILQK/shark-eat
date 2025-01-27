using System;
using UnityEngine;

public class CenterFish : Fish
{
    private Vector2 startPosition; // Posición inicial del pez
    private bool movingToCenter = true; // Indica si el pez está yendo hacia el centro
    private Vector2 centerPosition = Vector2.zero; // Centro de la pantalla (0, 0)

    private bool scaled = false;
    // Inicializa el pez con posición de inicio y dirección inicial
    public override void Initialize(Vector2 spawnPosition, Vector2 moveDirection)
    {
        base.Initialize(spawnPosition, moveDirection);
        startPosition = spawnPosition; // Guarda la posición inicial
        direction = (centerPosition - (Vector2)transform.position).normalized; // Direcciona hacia el centro
    }

    // Movimiento hacia el centro y regreso
    protected override void Update()
    {
        if (movingToCenter)
        {
            // Mover hacia el centro
            transform.Translate(direction * (speed * Time.deltaTime));

            // Comprobar si está cerca del centro
            if (Vector2.Distance(transform.position, centerPosition) < 0.1f)
            {
                movingToCenter = false; // Cambiar dirección
                direction = (startPosition - (Vector2)transform.position).normalized;
            }
        }
        else
        {
            if (!scaled)
            {
                if(transform.localScale.x > 0)
                    transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
                else
                {
                    transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), Mathf.Abs(transform.localScale.y));
                }
                scaled = true;
            }
            // Mover hacia la posición inicial
            transform.Translate(direction * (speed * Time.deltaTime));

            // Comprobar si está cerca de la posición inicial
            if (Vector2.Distance(transform.position, startPosition) < 0.1f)
            {
                // Detener el pez o reiniciar su ciclo
                enabled = false; // Opcional: desactivar la actualización si ya no se necesita
            }
        }
    }
}