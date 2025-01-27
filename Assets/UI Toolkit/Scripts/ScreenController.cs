using UnityEngine;
using UnityEngine.UIElements;

public class ScreenController : MonoBehaviour
{
    private VisualElement _startMenu;
    private VisualElement _mainMenu;
    private VisualElement _settingsMenu;

    void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        _startMenu = root.Q("StartMenu");
        _mainMenu = root.Q("MainMenu");
        _settingsMenu = root.Q("SettingsMenu");

        HideAllMenus();

        ShowMenu(_mainMenu);

        var mainMenuController = new MainMenuController(_mainMenu);
        mainMenuController.OpenSettings = () =>
        {
            ShowMenu(_settingsMenu);
        };

        mainMenuController.BackToStartMenu = Application.Quit;

        var settingsMenuController = new SettingsMenuController(_settingsMenu);
        settingsMenuController.Back = () =>
        {
            ShowMenu(_mainMenu);
        };

        settingsMenuController.VolumeChanged = OnVolumeChanged;

        settingsMenuController.MusicToggled = OnMusicToggled;

        settingsMenuController.GyroscopeToggled = OnGyroscopeToggled;

    }
    private void OnVolumeChanged(float newVolume)
    {
        Debug.Log($"Nuevo volumen establecido: {newVolume}");
        // Call the AudioMixer to set the volume
    }

    private void OnMusicToggled(bool enabled)
    {
        //Call the AudioMixer to enable it
        Debug.Log($"Musica activada?: {enabled}");
    }

    private void OnGyroscopeToggled(bool enabled)
    {
        Debug.Log($"Giroscopio activado?: {enabled}");
    }

    private void HideAllMenus()
    {
        _startMenu.Display(false);
        _mainMenu.Display(false);
        _settingsMenu.Display(false);
    }

    private void ShowMenu(VisualElement menuToShow)
    {
        HideAllMenus();
        menuToShow.Display(true);
    }

    void Update()
    {
        if (Input.anyKeyDown && _startMenu.IsVisible())
        {
            ShowMenu(_mainMenu);
        }
    }
}
