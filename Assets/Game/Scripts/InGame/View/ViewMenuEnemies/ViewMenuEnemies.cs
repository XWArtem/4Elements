using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenuEnemies : MonoBehaviour, IViewWindow
{
    [SerializeField] private Button buttonZombieFrame;
    [SerializeField] private Button buttonOgreFrame;
    [SerializeField] private Button buttonZombieOgreFrame;
    [SerializeField] private Button buttonBatFrame;
    [SerializeField] private Button buttonSpiderFrame;
    [SerializeField] private Button buttonGolemFrame;
    [SerializeField] private Button buttonRobotFrame;
    [Space]
    [SerializeField] private RectTransform arrowZombieFrame;
    [SerializeField] private RectTransform arrowOgreFrame;
    [SerializeField] private RectTransform arrowZombieOgreFrame;
    [SerializeField] private RectTransform arrowBatFrame;
    [SerializeField] private RectTransform arrowGolemFrame;
    [SerializeField] private RectTransform arrowSpiderFrame;
    [SerializeField] private RectTransform arrowRobotFrame;
    [Space]
    [SerializeField] private List<ViewMenuEnemyFrame> frames;

    private RectTransform zombieRect;
    private RectTransform ogreRect;
    private RectTransform zombieOgreRect;
    private RectTransform batRect;
    private RectTransform golemRect;
    private RectTransform spiderRect;
    private RectTransform robotRect;

    private List<ViewMenuFrameFlexible> enemySlots = new List<ViewMenuFrameFlexible>(7);

    public void Init()
    {
        zombieRect = buttonZombieFrame.GetComponent<RectTransform>();
        ogreRect = buttonOgreFrame.GetComponent<RectTransform>();
        zombieOgreRect = buttonZombieOgreFrame.GetComponent<RectTransform>();
        batRect = buttonBatFrame.GetComponent<RectTransform>();
        spiderRect = buttonSpiderFrame.GetComponent<RectTransform>();
        golemRect = buttonGolemFrame.GetComponent<RectTransform>();
        robotRect = buttonRobotFrame.GetComponent<RectTransform>();

        enemySlots.Add(new ViewMenuFrameFlexible(false, buttonZombieFrame, zombieRect, arrowZombieFrame, SettingsMenu.EnemyFrameOpenedHeight));
        enemySlots.Add(new ViewMenuFrameFlexible(false, buttonOgreFrame, ogreRect, arrowOgreFrame, SettingsMenu.EnemyFrameOpenedHeight));
        enemySlots.Add(new ViewMenuFrameFlexible(false, buttonZombieOgreFrame, zombieOgreRect, arrowZombieOgreFrame, SettingsMenu.EnemyFrameOpenedHeight));
        enemySlots.Add(new ViewMenuFrameFlexible(false, buttonBatFrame, batRect, arrowBatFrame, SettingsMenu.EnemyFrameOpenedHeight));
        enemySlots.Add(new ViewMenuFrameFlexible(false, buttonSpiderFrame, spiderRect, arrowSpiderFrame, SettingsMenu.EnemyFrameOpenedHeight));
        enemySlots.Add(new ViewMenuFrameFlexible(false, buttonGolemFrame, golemRect, arrowGolemFrame, SettingsMenu.EnemyFrameOpenedHeight));
        enemySlots.Add(new ViewMenuFrameFlexible(false, buttonRobotFrame, robotRect, arrowRobotFrame, SettingsMenu.EnemyFrameOpenedHeight));

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
