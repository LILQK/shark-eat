using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Slider = UnityEngine.UIElements.Slider;
using Toggle = UnityEngine.UIElements.Toggle;

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

    public SettingsMenuController(VisualElement root)
    {
        _backButton = root.Q<Button>("back-button");
        _toggleMusicCheckbox = root.Q<Toggle>("toggle-music");
        _toggleGyroscopeCheckbox = root.Q<Toggle>("toggle-gyroscope");
        _volumeSlider = root.Q<Slider>("volume-slider");


        _backButton.clicked += () => Debug.Log("Back Button Clicked");
        _toggleGyroscopeCheckbox.RegisterValueChangedCallback(evt =>
        {
            _onGyroscopeToggled(evt.newValue);
            Debug.Log($"Gyroscope toggled: {(evt.newValue ? "Enabled" : "Disabled")}");
        });

        _toggleMusicCheckbox.RegisterValueChangedCallback(evt =>
        {
            _onMusicToggled(evt.newValue);
            Debug.Log($"Music toggled: {(evt.newValue ? "Enabled" : "Disabled")}");
        });

        _volumeSlider.RegisterCallback<ChangeEvent<float>>(evt =>
        {
            Debug.Log("New value" + evt.newValue);
            _onVolumeChanged?.Invoke(evt.newValue);
        });

    }

}
