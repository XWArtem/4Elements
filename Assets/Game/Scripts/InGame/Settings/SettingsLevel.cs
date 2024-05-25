using System;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLevel : MonoBehaviour
{
    public static Action OnFlowSpeedChanged;

    [SerializeField][Tooltip("0.4f is a common value")] private float enemySpawnDelayMin;
    [SerializeField][Tooltip("0.6f is a common value")] private float enemySpawnDelayMax;
    [SerializeField] private string menuSceneName;
    [SerializeField] private string tdSceneName;

    public static float EnemySpawnDelayMin { get; private set; }
    public static float EnemySpawnDelayMax { get; private set; }

    public static int TotalEnemiesToWin { get; private set; }

    private static float customTimeFlow;
    public static float CustomTimeFlow
    {
        get
        {
            return customTimeFlow;
        }
        set
        {
            if (value == 1f || value == 2.5f)
            customTimeFlow = value;
            OnFlowSpeedChanged?.Invoke();
        }
    }
    public static string MenuSceneName { get; private set; }
    public static string TDSceneName { get; private set; }

    public void Init(int currentLvlIndex)
    {
        EnemySpawnDelayMin = enemySpawnDelayMin;
        EnemySpawnDelayMax = enemySpawnDelayMax;

        TotalEnemiesToWin = 0;
        List<EnemyWave> enemyWaves = DataStorageSingletone.Instance.DataStorageEnemies.EnemyWavesOnLevel[currentLvlIndex];
        foreach (EnemyWave wave in enemyWaves)
        {
            TotalEnemiesToWin += wave.Count;
        }

        Debug.Log("TotalEnemiesToWin = " + TotalEnemiesToWin);

        CustomTimeFlow = 1.0f;

        MenuSceneName = menuSceneName;
        TDSceneName = tdSceneName;
    }
}