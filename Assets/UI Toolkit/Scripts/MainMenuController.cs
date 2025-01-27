using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController
{
    public Action OpenSettings { set => _settingsButton.clicked += value; }
    public Action BackToStartMenu { set => _quitButton.clicked += value; }
    public Action<string> _onTextChanged;

    private Button _settingsButton;
    private Button _startButton;
    private Button _quitButton;
    private TextField _serialText;
    
    public MainMenuController(VisualElement root)
    {
        
        _settingsButton = root.Q<Button>("settings-button");
        _startButton = root.Q<Button>("start-button");
        _quitButton = root.Q<Button>("quit-button");
        _serialText = root.Q<TextField>("serialport");


        _settingsButton.clicked += () => Debug.Log("Settings Button Clicked");
        _startButton.clicked += () => SceneManager.LoadScene(1);
        _quitButton.clicked += () => Debug.Log("Quit Button Clicked");
        _serialText.RegisterValueChangedCallback(evt =>
        {
            Debug.Log("New value" + evt.newValue);
            _onTextChanged?.Invoke(evt.newValue);
            GlobalConfig.serialPortName = evt.newValue;
        });
        
        _serialText.value = GlobalConfig.serialPortName;

    }
}
