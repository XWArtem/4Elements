using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataStorageSingletone : MonoBehaviour
{
    public static DataStorageSingletone Instance;

    public readonly DataStorageTowers DataStorageTowers = new DataStorageTowers();
    public readonly DataStorageEnemies DataStorageEnemies = new DataStorageEnemies();
    public readonly DataStorageResources DataStorageResources = new DataStorageResources();

    /*[HideInInspector]*/ public int CurrentLevelIndex;
    /*[HideInInspector]*/ public int MaxLevelIndexOpened;
    private int castleHP;
    public int CastleHP
    {
        get
        {
            return castleHP;
        }
        set
        {
            if (value <= 0)
            {
                Conditions.OnCastleDamaged?.Invoke();
                castleHP = 0;
                Conditions.OnGameOver?.Invoke();
            }
            else
            {
                castleHP = value;
                Debug.Log("castleHP = " + castleHP);
            }
        }
    }

    public void Init()
    {
        CurrentLevelIndex = UserSettingsStorage.Instance.CurrentLevelIndex;
        MaxLevelIndexOpened = UserSettingsStorage.Instance.MaxLevelIndexOpened;

        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        CastleHP = 300; // 2sec * 50 fixedUpdates = 100 fixedUpdates. 100*3 = 300
    }
}
