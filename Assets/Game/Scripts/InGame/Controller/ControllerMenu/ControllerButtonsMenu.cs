using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerButtonsMenu : MonoBehaviour
{
    [SerializeField] private Button buttonSettings;
    [SerializeField] private List<Button> buttonsEncyclopedia;
    [SerializeField] private Button buttonGoToMenu;
    [SerializeField] private Button buttonCloseEncyclopedia;
    [SerializeField] private Button buttonCloseSettings;
    [SerializeField] private Button buttonPlay;

    [SerializeField] private Button buttonDefenseTowersScreen;
    [SerializeField] private Button buttonSupportTowersScreen;
    [SerializeField] private Button buttonEnemiesScreen;
    [SerializeField] private Button buttonResources;

    [SerializeField] private Button buttonOpenConfirmation;
    [SerializeField] private Button buttonCloseConfirmation;
    [SerializeField] private Button buttonResetProgress;
    [SerializeField] private Button buttonReturnToSettings;

    private List<Button> allButtonsOnScene = new List<Button>();
    //
    [SerializeField] private ControllerMenuScreen controllerMenuPopup;
    [SerializeField] private ViewMenuSettings viewMenuSettings;

    private void OnEnable()
    {
        allButtonsOnScene.Add(buttonSettings);
        foreach(var button in buttonsEncyclopedia)
        {
            allButtonsOnScene.Add(button);
        }
        allButtonsOnScene.Add(buttonCloseSettings);
        allButtonsOnScene.Add(buttonPlay);
        allButtonsOnScene.Add(buttonCloseEncyclopedia); 
        allButtonsOnScene.Add(buttonDefenseTowersScreen);
        allButtonsOnScene.Add(buttonSupportTowersScreen);
        allButtonsOnScene.Add(buttonEnemiesScreen);
        allButtonsOnScene.Add(buttonResources);
        
        allButtonsOnScene.Add(buttonOpenConfirmation);
        allButtonsOnScene.Add(buttonCloseConfirmation);
        allButtonsOnScene.Add(buttonResetProgress);
        allButtonsOnScene.Add(buttonReturnToSettings);
        allButtonsOnScene.Add(buttonGoToMenu);


        // unique actions
        buttonSettings.onClick.AddListener(() => controllerMenuPopup.OpenPopupSettings());
        foreach (var button in buttonsEncyclopedia)
        {
            button.onClick.AddListener(() => controllerMenuPopup.OpenEncyclopedia());
        }
        buttonCloseEncyclopedia.onClick.AddListener(() => controllerMenuPopup.CloseEncyclopedia());
        buttonCloseSettings.onClick.AddListener(() => controllerMenuPopup?.ClosePopupSettings());
        buttonDefenseTowersScreen.onClick.AddListener(() => controllerMenuPopup?.OpenDefenseTowersScreen());
        buttonSupportTowersScreen.onClick.AddListener(() => controllerMenuPopup?.OpenSupportTowersScreen());
        buttonEnemiesScreen.onClick.AddListener(() => controllerMenuPopup?.OpenEnemiesScreen());
        buttonResources.onClick.AddListener(() => controllerMenuPopup?.OpenResourcesScreen());

        buttonOpenConfirmation.onClick.AddListener(() => viewMenuSettings.OpenConfirmationFrame());
        buttonCloseConfirmation.onClick.AddListener(() => viewMenuSettings.CloseConfirmationFrame());
        buttonResetProgress.onClick.AddListener(() => UserSettingsStorage.Instance.ResetProgress());
        buttonResetProgress.onClick.AddListener(() => viewMenuSettings.CloseConfirmationFrame());
        buttonReturnToSettings.onClick.AddListener(() => viewMenuSettings.CloseConfirmationFrame());

        buttonPlay.onClick.AddListener(() => controllerMenuPopup.OpenLevels());
        buttonGoToMenu.onClick.AddListener(() => controllerMenuPopup.CloseLevels());
    }

    private void Start()
    {
        // common for all
        foreach (var button in allButtonsOnScene)
        {
            button.onClick.AddListener(AudioManager.Instance.PlayTapSound);
        }
    }

    private void OnDisable()
    {
        foreach (var button in allButtonsOnScene)
        {
            button.onClick.RemoveAllListeners();
        }
        allButtonsOnScene.Clear();
    }
}