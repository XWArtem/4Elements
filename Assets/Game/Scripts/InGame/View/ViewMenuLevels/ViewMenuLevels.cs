using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ViewMenuLevels : MonoBehaviour
{
    [SerializeField] private GameObject levelFramePrefab;
    [SerializeField] private RectTransform parentRect;

    private List<ViewMenuLevelFrame> levelFrames = new List<ViewMenuLevelFrame>();

    public static bool LevelSelectionLocked;
    private bool initialized;

    private void Awake()
    {
        LevelSelectionLocked = false;

        string stars = UserSettingsStorage.Instance.Stars;
        Debug.Log(stars);
        List<int> starsAll = stars.Split(',').Select(Int32.Parse).ToList();

        for (int i = 1; i < 31;  i++)
        {
            var newFrame = Instantiate(levelFramePrefab, parentRect);
            newFrame.name = (i).ToString();
            newFrame.GetComponent<ViewMenuLevelFrame>().InitFrame(i, UserSettingsStorage.Instance.MaxLevelIndexOpened >= i, starsAll[i-1]);
            levelFrames.Add(newFrame.GetComponent<ViewMenuLevelFrame>());
        }
        initialized = true;
    }

    private void OnEnable()
    {
        if (!initialized) return;

        string stars = UserSettingsStorage.Instance.Stars;
        List<int> starsAll = stars.Split(',').Select(Int32.Parse).ToList();

        for (int i = 1; i < 31; i++)
        {
            levelFrames[i - 1].UpdateFrame(i, UserSettingsStorage.Instance.MaxLevelIndexOpened >= i, starsAll[i - 1]);
        }
    }
}
