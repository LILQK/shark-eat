using UnityEngine;

public class FastFish : Fish
{
    public override void Initialize(Vector2 spawnPosition, Vector2 moveDirection)
    {
        base.Initialize(spawnPosition, moveDirection);
        speed = 5f; // Peces rápidos tienen mayor velocidad
    }
}