using UnityEngine;

public class Orca : Fish
{
    private Collider2D orcaCollider; // Referencia al Collider2D
    public SpriteRenderer spriteRenderer; // Referencia al SpriteRenderer
    public bool returning = false; // Indica si la orca está regresando
    private float screenWidth; // Ancho de la pantalla en unidades del mundo
    public Sprite backSprite;
    public Sprite frontSprite;
    public override void Initialize(Vector2 spawnPosition, Vector2 moveDirection)
    {
        base.Initialize(spawnPosition, moveDirection);

        // Obtener referencias a los componentes necesarios
        orcaCollider = GetComponent<Collider2D>();

        // Desactivar el collider al inicio
        if (orcaCollider != null)
            orcaCollider.enabled = false;

        // Configurar opacidad inicial a mitad
        if (spriteRenderer != null)
            spriteRenderer.color = new Color(1f, 1f, 1f, 0.6f);

        // Obtener el ancho de la pantalla en unidades del mundo
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        
        spriteRenderer.sprite = backSprite;
        spriteRenderer.sortingOrder = -1;
    }

    protected override void Update()
    {
        base.Update();

        if (!returning)
        {
            // Comprobar si ha salido completamente por el lado contrario
            if ((direction.x > 0 && transform.position.x > screenWidth + 1f) || // Va hacia la derecha
                (direction.x < 0 && transform.position.x < -screenWidth - 1f))  // Va hacia la izquierda
            {
                spriteRenderer.sprite = frontSprite;
                // Cambiar dirección
                direction = -direction;
                returning = true;

                // Reactivar el Collider2D
                if (orcaCollider != null)
                    orcaCollider.enabled = true;

                // Restaurar la opacidad
                if (spriteRenderer != null)
                    spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

                // Invertir la escala para reflejar la dirección
                transform.localScale = new Vector3(
                    -transform.localScale.x, 
                    transform.localScale.y, 
                    transform.localScale.z
                );
                
                spriteRenderer.sortingOrder = 0;

            }
        }
    }
}
