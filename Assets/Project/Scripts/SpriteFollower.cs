using UnityEngine;

public class SpriteFollower : MonoBehaviour
{
    public Transform target; // Referencia al objeto vacío que tiene la lógica de movimiento
    public float followSpeed = 5f; // Velocidad de seguimiento
    public float rotationSpeed = 5f; // Velocidad de suavizado para la rotación
    private bool facingRight = true; // Indica si el sprite está mirando a la derecha

    void Update()
    {
        if (target == null) return;

        // Seguir suavemente al target
        Vector3 newPosition = Vector3.Lerp(transform.position, target.position, Time.deltaTime * followSpeed);
        transform.position = newPosition;

        // Calcular la dirección hacia el target
        Vector2 direction = target.position - transform.position;

        if (direction.sqrMagnitude > 0.01f) // Evitar cálculos si la distancia es muy pequeña
        {
            // Calcular el ángulo hacia el target
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Suavizar la rotación hacia el ángulo objetivo
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);

            // Invertir el sprite si cambia de dirección en el eje X
            if (direction.x > 0 && !facingRight)
            {
                FlipSprite();
            }
            else if (direction.x < 0 && facingRight)
            {
                FlipSprite();
            }
        }
    }

    // Método para invertir el sprite
    private void FlipSprite()
    {
        facingRight = !facingRight; // Cambiar el estado de la dirección
        Vector3 localScale = transform.localScale;
        localScale.x *= -1; // Invertir la escala en el eje X
        transform.localScale = localScale;

        // Alinear el sprite para que siempre mire en la dirección correcta
        float currentRotation = transform.eulerAngles.z; // Obtener la rotación actual
        transform.rotation = Quaternion.Euler(0, 0, currentRotation); // Resetear la rotación a la correcta
    }
}
