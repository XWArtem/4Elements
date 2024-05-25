using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleDIMenu : MonoBehaviour
{
    [SerializeField] private SettingsMenu settingsMenu;
    [SerializeField] private ControllerMenuScreen controllerMenuScreen;
    [SerializeField] private ViewMenuLightFading viewMenuLightFading;
    [SerializeField] private DataStorageSingletone dataStorage;
    [SerializeField] private SettingsProject settingsProject;
    [SerializeField] private ViewMenuDefenseTowers viewMenuDefenseTowers;
    [SerializeField] private ViewMenuSupportTowers viewMenuSupportTowers;
    [SerializeField] private ViewMenuEnemies viewMenuEnemies;
    [SerializeField] private ViewMenuResources viewMenuResources;
    [SerializeField] private ViewMenuSettings viewMenuSettings;

    private void Awake()
    {
        settingsProject.Init();
        settingsMenu.Init();
        dataStorage.Init();
        controllerMenuScreen.Init(viewMenuDefenseTowers, viewMenuSupportTowers, viewMenuEnemies, viewMenuResources);
        viewMenuLightFading.Init();

        viewMenuDefenseTowers.Init();
        viewMenuSupportTowers.Init();
        viewMenuEnemies.Init();
        viewMenuSettings.Init();
    }
}
