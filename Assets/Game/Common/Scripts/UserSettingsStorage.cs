using System;
using Newtonsoft.Json;
using UnityEngine;
using static UnityEngine.PlayerPrefs;

public class UserSettingsStorage {
    private static UserSettingsStorage _instance;

    public static UserSettingsStorage Instance {
        get {
            _instance ??= new UserSettingsStorage();
            return _instance;
        }
    }

    public int Diamond
    {
        get => Get("DiamondTD", 0);
        set => Set("DiamondTD", value);
    }

    public int Metal {
        get => Get("MetalTD", 0);
        set => Set("MetalTD", value);
    }

    public string Stars
    {
        get => PlayerPrefs.GetString("StarsTD", "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1");
        set => PlayerPrefs.SetString("StarsTD", value);
    }

    public int CurrentLevelIndex
    {
        get => Get("CurrentLevelIndex", 1);
        set
        {
            if (value >= 0 && value <= 30)
            {
                Set("CurrentLevelIndex", value);
                if (MaxLevelIndexOpened < CurrentLevelIndex)
                {
                    MaxLevelIndexOpened = CurrentLevelIndex;
                }
            }
        }
    }

    public int MaxLevelIndexOpened
    {
        get => Get("MaxLevelIndexOpened", 1);
        set
        {
            if (value >= 0 && value <= 30)
            {
                Set("MaxLevelIndexOpened", value);
            }
        }
    }

    public bool SoundState {
        get => Get("soundState", true);
        set => Set("soundState", value);
    }

    public bool MusicState {
        get => Get("musicState", true);
        set => Set("musicState", value);
    }

    public float MusicVolume {
        get => Get("musicVolume", 1.0f);
        set => Set("musicVolume", value);
    }

    public float SoundVolume {
        get => Get("soundVolume", 1.0f);
        set => Set("soundVolume", value);
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("CurrentLevelIndex", 1);
        //CurrentLevelIndex = 1;
        PlayerPrefs.SetInt("MaxLevelIndexOpened", 1);
        //MaxLevelIndexOpened = 1;
        Stars = "1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1";
    }

    #region private common methods

    private static void Set(string key, object value) {
        if (value == null) return;

        var serializedObj = JsonConvert.SerializeObject(value);
        SetString(key, serializedObj);
        Save();
    }

    private static T Get<T>(string key, T defaultValue = default) {
        object value = default(T);
        try {
            var data = GetString(key, defaultValue.ToString());
            if (!string.IsNullOrEmpty(data)) {
                value = JsonConvert.DeserializeObject<T>(data);
            }
        } catch (Exception) {
            DeleteKey(key);
        }

        return (T) value;
    }

    #endregion
}
