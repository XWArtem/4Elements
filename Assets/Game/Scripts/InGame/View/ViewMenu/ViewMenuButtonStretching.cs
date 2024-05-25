using DG.Tweening;
using System.Collections;
using UnityEngine;

public class ViewMenuButtonStretching : MonoBehaviour
{
    private RectTransform rect;
    private float baseTimeDelay = 6f;
    [SerializeField] private float onStartTimeDelay;
    [SerializeField] private float scaleMultiplier;

    private void Start()
    {
        rect = GetComponent<RectTransform>();
        StartCoroutine(StretchingRoutine());
    }

    private void SizeUp()
    {
        rect.DOScale(new Vector2(scaleMultiplier, scaleMultiplier), 1f).SetEase(Ease.InBack, 0.5f).OnComplete(SizeDown);
    }

    private void SizeDown()
    {
        rect.DOScale(Vector2.one, 1f).SetEase(Ease.OutBack, 0.5f);
    }

    private IEnumerator StretchingRoutine()
    {
        yield return new WaitForSeconds(onStartTimeDelay);
        while (true)
        {
            SizeUp();
            yield return new WaitForSeconds(baseTimeDelay);
        }
    }
}