using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenuFrameFlexible
{
    private bool frameOpened;
    private readonly Button actionButton;
    private readonly RectTransform frameRect;
    private RectTransform arrowRect;
    private float frameOpenedHeight;

    public void TriggerMenuSlot()
    {
        if (frameOpened) ShrinkFrame();
        else ExpandFrame();
    }

    private void ExpandFrame()
    {
        frameOpened = !frameOpened;
        frameRect.DOSizeDelta(new Vector2(frameRect.anchoredPosition.x, frameOpenedHeight), SettingsMenu.FrameTransitionLength);
        arrowRect.DOLocalRotate(new Vector3(0f, 0f, -90f), SettingsMenu.FrameTransitionLength);
    }

    private void ShrinkFrame()
    {
        frameOpened = !frameOpened;
        frameRect.DOSizeDelta(new Vector2(frameRect.anchoredPosition.x, SettingsMenu.CommonFrameClosedHeight), SettingsMenu.FrameTransitionLength);
        arrowRect.DOLocalRotate(new Vector3(0f, 0f, 0f), SettingsMenu.FrameTransitionLength);
    }

    public ViewMenuFrameFlexible(bool isOpened, Button actionBtn, RectTransform rect, RectTransform arrowRect, float frameOpenedHeight)
    {
        frameOpened = isOpened;
        actionButton = actionBtn;
        actionButton.onClick.AddListener(() => TriggerMenuSlot());
        frameRect = rect;
        this.arrowRect = arrowRect;
        this.frameOpenedHeight = frameOpenedHeight;

        ShrinkFrame();
        frameOpened = !frameOpened;
    }
}
