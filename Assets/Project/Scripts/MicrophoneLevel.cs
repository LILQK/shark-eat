using System;
using UnityEngine;

public class MicrophoneLevel : MonoBehaviour
{
    public string microphoneName; // Nombre del micrófono que quieres usar (puedes dejarlo vacío para usar el predeterminado)
    public float sensitivity = 100.0f; // Sensibilidad para ajustar el nivel
    public float loudness; // Nivel actual del micrófono

    private AudioClip micClip;

    void Start()
    {
        // Obtener el nombre del micrófono predeterminado si no se especifica uno
        if (string.IsNullOrEmpty(microphoneName))
        {
            if (Microphone.devices.Length > 0)
            {
                microphoneName = Microphone.devices[0];
                Debug.Log($"Usando micrófono predeterminado: {microphoneName}");
            }
            else
            {
                Debug.LogError("No se encontraron micrófonos en el sistema.");
                return;
            }
        }

        // Iniciar captura del micrófono
        StartMicrophone();
    }

    void Update()
    {
        // Actualizar el nivel de entrada del micrófono
        if (micClip != null && Microphone.IsRecording(microphoneName))
        {
            loudness = GetMicLevel() * sensitivity;
        }
    }

    void StartMicrophone()
    {
        int sampleRate = 44100; // Frecuencia de muestreo estándar
        micClip = Microphone.Start(microphoneName, true, 1, sampleRate); // Clip de 1 segundo en bucle
        Debug.Log($"Iniciando captura del micrófono: {microphoneName}");
    }

    float GetMicLevel()
    {
        if (micClip == null)
            return 0;

        // Leer los datos del clip
        float[] data = new float[256];
        int micPosition = Microphone.GetPosition(microphoneName) - 256; // Última posición del buffer
        if (micPosition < 0) return 0;

        micClip.GetData(data, micPosition);

        // Calcular el nivel RMS (Root Mean Square)
        float sum = 0;
        foreach (float sample in data)
        {
            sum += sample * sample;
        }
        return Mathf.Sqrt(sum / data.Length);
    }

    void OnDisable()
    {
        // Detener el micrófono cuando se desactiva el script
        if (Microphone.IsRecording(microphoneName))
        {
            Microphone.End(microphoneName);
            Debug.Log("Captura del micrófono detenida.");
        }
    }

    private void OnGUI()
    {
        
    }
}
