using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenuSupportTowers : MonoBehaviour, IViewWindow
{
    [SerializeField] private Button buttonSpeedTower;
    [SerializeField] private Button buttonDistanceTower;
    [SerializeField] private Button buttonPowerTower;
    [Space]
    [SerializeField] private RectTransform arrowSpeedTower;
    [SerializeField] private RectTransform arrowDistanceTower;
    [SerializeField] private RectTransform arrowPowerTower;
    [Space]
    [SerializeField] private List<ViewMenuSupportTowerFrame> frames;

    private RectTransform fireSpeedRect;
    private RectTransform airDistanceRect;
    private RectTransform waterPowerRect;

    List<ViewMenuFrameFlexible> towerSlots = new List<ViewMenuFrameFlexible>(3);

    public void Init()
    {
        fireSpeedRect = buttonSpeedTower.GetComponent<RectTransform>();
        airDistanceRect = buttonDistanceTower.GetComponent<RectTransform>();
        waterPowerRect = buttonPowerTower.GetComponent<RectTransform>();

        towerSlots.Add(new ViewMenuFrameFlexible(false, buttonSpeedTower, fireSpeedRect, arrowSpeedTower, SettingsMenu.SupportTowerFrameOpenedHeight));
        towerSlots.Add(new ViewMenuFrameFlexible(false, buttonDistanceTower, airDistanceRect, arrowDistanceTower, SettingsMenu.SupportTowerFrameOpenedHeight));
        towerSlots.Add(new ViewMenuFrameFlexible(false, buttonPowerTower, waterPowerRect, arrowPowerTower, SettingsMenu.SupportTowerFrameOpenedHeight));

        foreach (var frame in frames)
        {
            frame.Init();
        }

        WindowHide();
    }

    public void WindowShow()
    {
        gameObject.SetActive(true);
    }

    public void WindowHide()
    {
        gameObject.SetActive(false);
    }
}