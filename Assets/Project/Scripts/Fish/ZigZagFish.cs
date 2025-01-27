using UnityEngine;

public class ZigZagFish : Fish
{
    private float frequency = 2f;
    private float magnitude = 0.5f;
    private Vector2 initialPosition;

    public float leftBound = 0.05f;
    public float rightBound = 0.95f;
    public float topBound = 0.1f;
    public float bottomBound = 0.95f;

    private Camera cam;

    public override void Initialize(Vector2 spawnPosition, Vector2 moveDirection)
    {
        spawnPosition.y = 1.5f;
        base.Initialize(spawnPosition, moveDirection);
        initialPosition = spawnPosition;

        // Obtén la referencia a la cámara principal
        cam = Camera.main;
    }

    protected override void Update()
    {
        // Calcula el movimiento en zigzag
        float xOffset = Mathf.Sin(Time.time * frequency) * magnitude;
        Vector2 offset = new Vector2(0, xOffset);

        // Aplica el movimiento
        transform.Translate((direction + offset) * (speed * Time.deltaTime));

        // Calcula los límites de movimiento en coordenadas del mundo
        float _leftBound = cam.ViewportToWorldPoint(new Vector3(leftBound, 0, 0)).x;
        float _rightBound = cam.ViewportToWorldPoint(new Vector3(rightBound, 0, 0)).x;
        float _topBound = cam.ViewportToWorldPoint(new Vector3(0, topBound, 0)).y;
        float _bottomBound = cam.ViewportToWorldPoint(new Vector3(0, bottomBound, 0)).y;

        // Limita la posición del pez dentro de los límites
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, _leftBound, _rightBound);
        pos.y = Mathf.Clamp(pos.y, _topBound, _bottomBound);
        transform.position = pos;
    }
}