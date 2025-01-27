using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public Bubble bubblePrefab;

    private Bubble currentBubble = null;

    public float cooldown = 0.5f;
    private float lastShootTime = 0;

    public float loudnessThreshold = 0.1f; // Umbral de loudness para activar el disparo

    MicrophoneLevel microphone;

    public bool isInflating = false;
    public bool onCooldown = false;

    public Transform[] hands;
    public Transform canon;
    
    public float leftLimit = -5f;
    public float rightLimit = 5f;
    
    public SerialReader serialReader;

    [Header("References")] public SpriteRenderer handL;
    public SpriteRenderer handR;
    public SpriteRenderer canonR;
    public Sprite handLog;
    public Sprite handRog;
    public Sprite handLu;
    public Sprite handRu;
    public Sprite canonOg;
    public Sprite canonU;
    public AudioSource grunt;
    
    
    private void Start()
    {
        microphone = GetComponent<MicrophoneLevel>();
        canon.DOScaleX(1.08f,1f).From(1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        canon.DOScaleY(0.92f,1f).From(1f).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        int i = 0;
        foreach (var hand in hands)
        {
            hand.DOLocalMoveY(hand.localPosition.y +.04f,0.5f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic).From(hand.localPosition.y - 0.5f).SetDelay(i / 2f);
            i++;
        }
    }

    public enum InputType
    {
        arrows,
        wasd,
        microphone
    }

    public InputType inputType = InputType.arrows;

    private void Update()
    {
        MoveInputs();
        ShootInputs();
    }

    private void ShootInputs()
    {
        if (inputType == InputType.microphone)
        {
            //MicrophoneInput();
            NewMicInput();
        }
        else
        {
            KeyboardInput();
        }
    }

    private void NewMicInput()
    {
        if (microphone.loudness > loudnessThreshold && !onCooldown && !isInflating)
        {
            StartInflating();
            canonR.sprite = canonU;
            handR.sprite = handRu;
            handL.sprite = handLu;
        }
        else if (isInflating && (microphone.loudness <= loudnessThreshold || currentBubble.size > currentBubble.maxSize))
        {
            ReleaseBubble();
            StartCoroutine(WaitAndChange());

        }

        // Si está inflando, agranda la burbuja
        if (isInflating && currentBubble != null && currentBubble.size < currentBubble.maxSize)
        {
            InflateBubble();
        }
    }

    private void InflateBubble()
    {
        currentBubble.size += 1f * Time.deltaTime;
    }

    IEnumerator WaitAndChange()
    {
        yield return new WaitForSeconds(1f);
        canonR.sprite = canonOg;
        handR.sprite = handRog;
        handL.sprite = handLog;
    }

    private void ReleaseBubble()
    {
        isInflating = false;
        currentBubble.Init();
        currentBubble = null;
        lastShootTime = Time.time;
        StartCoroutine(Cooldown());
        Debug.Log("Shoot bubble");
    }

    private void StartInflating()
    {
        grunt.Play();
        //if (isInflating) return;
        isInflating = true;
        // Si no hay burbuja actual, crea una nueva
        var b = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
        b.team = 0; // Ajusta el equipo si es necesario
        currentBubble = b;
        b.size = 0.35f;
    }
    
    
    private System.Collections.IEnumerator Cooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(cooldown);
        onCooldown = false;
    }

    private void MicrophoneInput()
    {

        // Si el nivel de loudness supera el umbral, incrementa el tamaño de la burbuja
        if (microphone.loudness > loudnessThreshold)
        {
            if (currentBubble != null)
            {
                if (currentBubble.size > currentBubble.maxSize) return;

                currentBubble.size += 2f * Time.deltaTime;
            }
            else
            {
                // Si no hay burbuja actual, crea una nueva
                var b = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
                b.team = 0; // Ajusta el equipo si es necesario
                currentBubble = b;
            }
        }


        // Si el nivel de loudness cae por debajo del umbral, lanza la burbuja
        if (microphone.loudness <= loudnessThreshold && currentBubble != null)
        {
            currentBubble.Init();
            currentBubble = null;
            lastShootTime = Time.time;
            Debug.Log("Shoot bubble");
        }
    }

    private void KeyboardInput()
    {

        if (Input.GetKey(KeyCode.W) && inputType == InputType.wasd || inputType == InputType.arrows && Input.GetKey(KeyCode.UpArrow))
        {
            if (currentBubble != null)
            {
                if (currentBubble.size > currentBubble.maxSize) return;

                currentBubble.size += 2f * Time.deltaTime;
            }
        }
        if (!(Time.time - lastShootTime > cooldown)) return;

        if (Input.GetKeyDown(KeyCode.W) && inputType == InputType.wasd || inputType == InputType.arrows && Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentBubble == null)
            {
                var b = Instantiate(bubblePrefab, transform.position, Quaternion.identity);
                b.team = inputType == InputType.arrows ? 1 : 0;
                currentBubble = b;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) && inputType == InputType.wasd || inputType == InputType.arrows && Input.GetKeyUp(KeyCode.UpArrow))
        {
            currentBubble.Init();
            currentBubble = null;
            lastShootTime = Time.time;
            Debug.Log("Shoot bubble");

        }
    }

    private void MoveInputs()
    {
        if(isInflating)return;
        
        var cam = Camera.main;
        float leftBound = cam.ViewportToWorldPoint(Vector3.zero).x;
        float rightBound = cam.ViewportToWorldPoint(Vector3.right).x;

        var pos = transform.position;
        if (Input.GetKey(KeyCode.A ) || serialReader.value3 < 450)
        {
            pos += Vector3.left * (Time.deltaTime * moveSpeed);
        }
        else if (Input.GetKey(KeyCode.D) || serialReader.value3 > 600)
        {
            pos += Vector3.right * (Time.deltaTime * moveSpeed);
        }

        pos.x = Mathf.Clamp(pos.x, leftLimit, rightLimit);

        transform.position = pos;
    }
    private void OnGUI()
    {
        // Ajusta la posición y tamaño de la barra según tu diseño
        float barWidth = 200f;
        float barHeight = 20f;
        float barX = 10f;
        float barY = 10f;

        // Escala el nivel de loudness a un rango entre 0 y 1
        float normalizedLoudness = Mathf.Clamp01(microphone.loudness);

        // Fondo de la barra
        GUI.color = Color.gray;
        GUI.Box(new Rect(barX, barY, barWidth, barHeight), GUIContent.none);

        // Nivel del micrófono (barra rellena)
        GUI.color = Color.green;
        GUI.Box(new Rect(barX, barY, barWidth * normalizedLoudness, barHeight), GUIContent.none);

        // Texto con el nivel de loudness
        GUI.color = Color.white;
        GUI.Label(new Rect(barX, barY + barHeight, barWidth, 20f), $"Mic Level: {microphone.loudness:F2}");
    }
    
    private void OnDrawGizmos()
    {
        // Dibuja los límites de movimiento en el Gizmo
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(leftLimit, transform.position.y - 1, 0), new Vector3(leftLimit, transform.position.y + 1, 0));
        Gizmos.DrawLine(new Vector3(rightLimit, transform.position.y - 1, 0), new Vector3(rightLimit, transform.position.y + 1, 0));
    }

}
