using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Globalization;
// For Serial
using System.IO.Ports;
// For compare array
using System.Linq;

public class SerialReader : MonoBehaviour {

    // Serial
    public string portName;
    public int baudRate = 9600;
    SerialPort arduinoSerial;

    public int sensor;
    private string buffer;
    public float value1 = 0f;
    public float value2 = 0f;
    public float value3 = 0f;
    public float value4 = 0f;
    public bool useGyroscope = false;
    void Start () {

        if (PlayerPrefs.GetInt("GyroscopeEnabled", 0) == 0)
        {
            useGyroscope = false;
            return;
        }

        useGyroscope = true;
        portName = GlobalConfig.serialPortName;
        // Open Serial port
        arduinoSerial = new SerialPort (portName, baudRate);
        // Set buffersize so read from Serial would be normal
        arduinoSerial.ReadTimeout = 2;
        arduinoSerial.DtrEnable = true;
        arduinoSerial.RtsEnable = true;;
        arduinoSerial.Open ();
    }

    void Update() {
        if(useGyroscope)
            ReadFromArduino ();
    }

    private void ReadFromArduino () {
        
        try {
            // Leer datos disponibles
                string data = arduinoSerial.ReadExisting();
                if (!string.IsNullOrEmpty(data))
                {
                    buffer += data; // Añade datos al buffer

                    // Procesar el mensaje completo si se encuentra delimitado
                    while (buffer.Contains("<") && buffer.Contains(">"))
                    {
                        int start = buffer.IndexOf("<");
                        int end = buffer.IndexOf(">");

                        if (start < end) // Mensaje válido
                        {
                            string completeMessage = buffer.Substring(start + 1, end - start - 1);
                            buffer = buffer.Substring(end + 1); // Eliminar mensaje procesado

                            // Procesar el mensaje completo

                            // Dividir el mensaje por la coma y asignar a variables
                            string[] values = completeMessage.Split(',');
                            if (values.Length == 4)
                            {
                                // Usar cultura para asegurar el punto flotante
                                CultureInfo culture = CultureInfo.InvariantCulture;

                                if (float.TryParse(values[0], NumberStyles.Float, culture, out value1) &&
                                    float.TryParse(values[1], NumberStyles.Float, culture, out value2) &&
                                    float.TryParse(values[2], NumberStyles.Float, culture, out value3) &&
                                    float.TryParse(values[3], NumberStyles.Float, culture, out value4))
                                {
                                    //Debug.Log(values[0] + " " + values[1] + " " + values[2] + " " + values[3] + " ");
                                }
                                else
                                {
                                    Debug.LogWarning("No se pudieron convertir los valores a float.");
                                }
                            }
                            else
                            {
                                Debug.LogWarning("El mensaje no tiene el formato esperado.");
                            }
                        }
                        else
                        {
                            // Eliminar datos corruptos
                            buffer = buffer.Substring(end + 1);
                        }
                    }
                }
        }
        catch (TimeoutException e) {
            Debug.LogWarning(e);
        }
    }
    
    void OnApplicationQuit()
    {
        if (arduinoSerial != null && arduinoSerial.IsOpen)
        {
            arduinoSerial.Close();
        }
    }
}