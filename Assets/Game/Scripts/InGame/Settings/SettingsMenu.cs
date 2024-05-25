using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] private float globalTransitionLength;
    [SerializeField] private float menuSlotTransitionLength;
    [SerializeField] [Tooltip("254f is a common value")] private float commonFrameClosedHeight;
    [SerializeField] [Tooltip("890f is a common value")] private float defenseTowerFrameOpenedHeight;
    [SerializeField] [Tooltip("650f is a common value")] private float supportTowerFrameOpenedHeight;
    [SerializeField] [Tooltip("686f is a common value")] private float enemyFrameOpenedHeight;
    [SerializeField] private string menuSceneName;
    [SerializeField] private string tdSceneName;



    public static float WindowsTransitionLength { get; private set; }
    public static float FrameTransitionLength { get; private set; }
    public static float CommonFrameClosedHeight { get; private set; }
    public static float DefenseTowerFrameOpenedHeight { get; private set; }
    public static float SupportTowerFrameOpenedHeight { get; private set; }
    public static float EnemyFrameOpenedHeight { get; private set; }
    public static string MenuSceneName { get; private set; }
    public static string TDSceneName { get; private set; }

    public void Init()
    {
        WindowsTransitionLength = menuSlotTransitionLength;
        FrameTransitionLength = menuSlotTransitionLength;
        CommonFrameClosedHeight = commonFrameClosedHeight;
        DefenseTowerFrameOpenedHeight = defenseTowerFrameOpenedHeight;
        SupportTowerFrameOpenedHeight = supportTowerFrameOpenedHeight;
        EnemyFrameOpenedHeight = enemyFrameOpenedHeight;
        MenuSceneName = menuSceneName;
        TDSceneName = tdSceneName;
    }
}
