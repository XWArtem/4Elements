using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenuDefenseTowers : MonoBehaviour, IViewWindow
{
    [SerializeField] private Button buttonFireTower;
    [SerializeField] private Button buttonAirTower;
    [SerializeField] private Button buttonWaterTower;
    [SerializeField] private Button buttonEarthTower;
    [Space]
    [SerializeField] private RectTransform arrowFireTower;
    [SerializeField] private RectTransform arrowAirTower;
    [SerializeField] private RectTransform arrowWaterTower;
    [SerializeField] private RectTransform arrowEarthTower;
    [Space]
    [SerializeField] private List<ViewMenuTowerFrame> frames;

    private RectTransform fireTowerRect;
    private RectTransform airTowerRect;
    private RectTransform waterTowerRect;
    private RectTransform earthTowerRect;

    private List<ViewMenuFrameFlexible> towerSlots = new List<ViewMenuFrameFlexible>(4);

    public void Init()
    {
        fireTowerRect = buttonFireTower.GetComponent<RectTransform>();
        airTowerRect = buttonAirTower.GetComponent<RectTransform>();
        waterTowerRect = buttonWaterTower.GetComponent<RectTransform>();
        earthTowerRect = buttonEarthTower.GetComponent<RectTransform>();

        towerSlots.Add(new ViewMenuFrameFlexible(false, buttonFireTower, fireTowerRect, arrowFireTower, SettingsMenu.DefenseTowerFrameOpenedHeight));
        towerSlots.Add(new ViewMenuFrameFlexible(false, buttonAirTower, airTowerRect, arrowAirTower, SettingsMenu.DefenseTowerFrameOpenedHeight));
        towerSlots.Add(new ViewMenuFrameFlexible(false, buttonWaterTower, waterTowerRect, arrowWaterTower, SettingsMenu.DefenseTowerFrameOpenedHeight));
        towerSlots.Add(new ViewMenuFrameFlexible(false, buttonEarthTower, earthTowerRect, arrowEarthTower, SettingsMenu.DefenseTowerFrameOpenedHeight));

        foreach(var frame in frames)
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