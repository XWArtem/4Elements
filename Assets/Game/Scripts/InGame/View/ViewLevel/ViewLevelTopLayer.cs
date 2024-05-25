using UnityEngine;
using UnityEngine.UI;

public class ViewLevelTopLayer : MonoBehaviour
{
    [SerializeField] private Button buttonSpeedUp;

    public void Init()
    {
        buttonSpeedUp.onClick.AddListener(SpeedUp);
    }

    private void SpeedUp()
    {
        if (SimpleDILevel.Instance.TimeScaleHandler.CustomTimeScale == 1f || SimpleDILevel.Instance.TimeScaleHandler.CustomTimeScale == 0f)
        {
            SimpleDILevel.Instance.TimeScaleHandler.SetTimeScale(3.5f);
        }
        else if (SimpleDILevel.Instance.TimeScaleHandler.CustomTimeScale == 3.5f)
        {
            SimpleDILevel.Instance.TimeScaleHandler.SetTimeScale(1f);
        }

        AudioManager.Instance.PlayTapSound();
    }

    private void OnDestroy()
    {
        buttonSpeedUp.onClick.RemoveAllListeners();
    }
}
