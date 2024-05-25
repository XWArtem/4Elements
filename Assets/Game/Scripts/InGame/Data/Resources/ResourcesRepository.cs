using System;
using UnityEngine;
using UnityEngine.Events;

public class ResourcesRepository : MonoBehaviour
{
    private int iron;
    private int diamond;

    public int Iron
    {
        get { return iron; }
        set { iron = value; OnResourcesChanged?.Invoke(); }
    }
    public int Diamond
    {
        get { return diamond; }
        set { diamond = value; OnResourcesChanged?.Invoke(); }
    }

    public UnityEvent OnResourcesChanged;
    public static Action<int> OnGetIronReward;
    public static Action<int> OnGetDiamondReward;

    public void Init(int lvlIndex)
    {
        OnResourcesChanged = new UnityEvent();

        Iron = DataStorageResources.ResourcesOnStart[lvlIndex].Iron;
        Diamond = DataStorageResources.ResourcesOnStart[lvlIndex].Diamond;

        OnGetIronReward += GetRewardIron;
        OnGetDiamondReward += GetDiamondReward;
    }

    private void OnDestroy()
    {
        OnGetIronReward -= GetRewardIron;
        OnGetDiamondReward -= GetDiamondReward;
    }

    private void GetRewardIron(int value)
    {
        Iron += value;
    }
    private void GetDiamondReward(int value)
    {
        Diamond += value;
    }
}