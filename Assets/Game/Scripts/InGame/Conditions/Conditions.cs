using System.Collections;
using UnityEngine;
using System;

public class Conditions : MonoBehaviour
{
    public static Action OnGameOver;
    public static Action OnGameWin;
    public static Action OnEnemyDie;
    public static Action OnCastleDamaged;

    private int enemiesLeftToWin;
    public int EnemiesLeftToWin
    {
        get
        {
            return enemiesLeftToWin;
        }
        set 
        { 
            enemiesLeftToWin = value;
            if (enemiesLeftToWin == 0)
            {
                Debug.Log("Win Round!");
                OnGameWin?.Invoke();
            }
        }
    }

    public void Init()
    {
        EnemiesLeftToWin = SettingsLevel.TotalEnemiesToWin;
    }

    private void OnEnable()
    {
        OnEnemyDie += EnemyDie;
    }

    private void OnDisable()
    {
        OnEnemyDie -= EnemyDie;
    }

    private void EnemyDie()
    {
        EnemiesLeftToWin--;
    }
}
