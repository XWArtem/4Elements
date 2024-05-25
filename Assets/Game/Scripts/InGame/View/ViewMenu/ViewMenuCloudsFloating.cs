using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewMenuCloudsFloating : MonoBehaviour
{
    [SerializeField] private RectTransform firstCloudFloating, secondCloudFloating, thirdCloudFloating, forthCloudFloating;

    private void Awake()
    {
        firstCloudFloating.DOAnchorPosX(-650f, 30f);
        secondCloudFloating.DOAnchorPosX(512f, 40f);
        thirdCloudFloating.DOAnchorPosX(350f, 100f);
        forthCloudFloating.DOAnchorPosX(-500f, 100f);
    }

}
