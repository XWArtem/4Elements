using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMenuParallaxBackground : MonoBehaviour
{
    public enum ParallaxDepth
    {
        bottom, middle, top
    }

    [SerializeField] private RectTransform parallaxBackground;
    private ParallaxDepth parallaxDepth = ParallaxDepth.bottom;
    private readonly Dictionary<ParallaxDepth, float> parallaxParams = new Dictionary<ParallaxDepth, float>()
    {
        {ParallaxDepth.bottom, 150f},
        {ParallaxDepth.middle, 0f},
        {ParallaxDepth.top, -150f},
    };

    public void SetParallaxBgLevel(ParallaxDepth depth)
    {
        parallaxDepth = depth;
        parallaxBackground.DOAnchorPosY(parallaxParams[depth], SettingsMenu.WindowsTransitionLength);
    }
}
