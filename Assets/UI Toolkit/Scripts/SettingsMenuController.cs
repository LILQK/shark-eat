using System;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingsMenuController
{
    public Action Back { set => _backButton.clicked += value; }
    public Action<bool> MusicToggled { set => _onMusicToggled = value; }
    public Action<bool> GyroscopeToggled { set => _onGyroscopeToggled = value; }
    public Action<float> VolumeChanged { set => _onVolumeChanged = value; }

    private Action<float> _onVolumeChanged;
    private Action<bool> _onMusicToggled;
    private Action<bool> _onGyroscopeToggled;

    private Button _backButton;
    private Toggle _toggleMusicCheckbox;
    private Toggle _toggleGyroscopeCheckbox;
    private Slider _volumeSlider;

    private const string GyroscopePrefKey = "GyroscopeEnabled";

    public SettingsMenuController(VisualElement root)
    {
        _backButton = root.Q<Button>("back-button");
        _toggleMusicCheckbox = root.Q<Toggle>("toggle-music");
        _toggleGyroscopeCheckbox = root.Q<Toggle>("toggle-gyroscope");
        _volumeSlider = root.Q<Slider>("volume-slider");

        // Asigna callback del botón "back"
        _backButton.clicked += () => Debug.Log("Back Button Clicked");

        // ----- Cargar la preferencia de gyroscopio -----
        // Obtener el valor guardado (si no existe, toma el valor 0).
        // Convierte 1 -> true, 0 -> false.
        bool gyroscopeEnabled = PlayerPrefs.GetInt(GyroscopePrefKey, 0) == 1;
        
        // Ajusta la UI (Toggle) según la preferencia guardada
        _toggleGyroscopeCheckbox.value = gyroscopeEnabled;
        GlobalConfig.useArduino = gyroscopeEnabled;

        // ----- Callback del Toggle de gyroscopio -----
        _toggleGyroscopeCheckbox.RegisterValueChangedCallback(evt =>
        {
            _onGyroscopeToggled?.Invoke(evt.newValue);
            GlobalConfig.useArduino = evt.newValue;

            // Guardar valor en PlayerPrefs (1 si es true, 0 si es false)
            PlayerPrefs.SetInt(GyroscopePrefKey, evt.newValue ? 1 : 0);
            // Recomendado para forzar el guardado inmediato
            PlayerPrefs.Save();

            Debug.Log($"Gyroscope toggled: {(evt.newValue ? "Enabled" : "Disabled")}");
        });

        // ----- Callback del Toggle de música -----
        _toggleMusicCheckbox.RegisterValueChangedCallback(evt =>
        {
            _onMusicToggled?.Invoke(evt.newValue);
            Debug.Log($"Music toggled: {(evt.newValue ? "Enabled" : "Disabled")}");
        });

        // ----- Callback del Slider de volumen -----
        _volumeSlider.RegisterCallback<ChangeEvent<float>>(evt =>
        {
            Debug.Log("New volume value: " + evt.newValue);
            _onVolumeChanged?.Invoke(evt.newValue);
        });
    }
}
