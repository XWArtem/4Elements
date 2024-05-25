using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenuLightFading : MonoBehaviour
{
    [SerializeField] private Image lightImage;

    public void Init()
    {
        LightFadeOff();
    }

    private void LightFadeOn()
    {
        lightImage.DOFade(1f, SettingsMenu.WindowsTransitionLength).OnComplete(LightFadeOff);
    }

    private void LightFadeOff()
    {
        lightImage.DOFade(0.8f, SettingsMenu.WindowsTransitionLength).OnComplete(LightFadeOn);
    }
}
