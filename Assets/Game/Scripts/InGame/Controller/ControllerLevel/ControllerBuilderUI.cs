using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerBuilderUI : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private RectTransform builderRect;
    [SerializeField] private RectTransform smallBuilderRect;

    [SerializeField] private RectTransform canvasRect;

    [SerializeField] private RectTransform builderExitField;

    [SerializeField] private List<ControllerBuilderButtonUI> builderControllers;

    [SerializeField] private ControllerBuilderButtonSell controllerBuilderButtonSell;
    [SerializeField] private ControllerBuilderButtonUpgrade controllerBuilderButtonUpgrade;

    private ControllerTile currentTile;

    private const float animationLength = 0.2f;
    private ResourcesRepository repository;

    public void Init(ResourcesRepository repository)
    {
        gameObject.SetActive(false);
        this.repository = repository;

        foreach (var controller in builderControllers)
        {
            controller.Init(this, repository);
        }

        controllerBuilderButtonSell.Init(this);
        controllerBuilderButtonUpgrade.Init(this, repository);
    }

    public void InvokeBuilder(ControllerTile targetTile)
    {
        foreach (var controller in builderControllers)
        {
            controller.UpdateView();
        }

        currentTile = targetTile;
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(targetTile.gameObject.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        (ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
        (ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

        builderRect.anchoredPosition = WorldObject_ScreenPosition;

        builderRect.localScale = new Vector3(0.05f, 0.05f);
        builderRect.DOScale(Vector3.one, animationLength).SetUpdate(true);
        builderRect.gameObject.SetActive(true);
        smallBuilderRect.gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    public void InvokeSmallBuilder(ControllerTile targetTile, int totalValue, int nextGradeCost, ResourceType resType)
    {
        controllerBuilderButtonSell.UpdateButton(totalValue, resType);
        controllerBuilderButtonUpgrade.UpdateButton(!targetTile.IsSupportTower, targetTile.TowerLevel, nextGradeCost, resType);

        currentTile = targetTile;
        Vector2 ViewportPosition = Camera.main.WorldToViewportPoint(targetTile.gameObject.transform.position);
        Vector2 WorldObject_ScreenPosition = new Vector2(
        (ViewportPosition.x * canvasRect.sizeDelta.x) - (canvasRect.sizeDelta.x * 0.5f),
        (ViewportPosition.y * canvasRect.sizeDelta.y) - (canvasRect.sizeDelta.y * 0.5f));

        smallBuilderRect.anchoredPosition = WorldObject_ScreenPosition;

        smallBuilderRect.localScale = new Vector3(0.05f, 0.05f);
        smallBuilderRect.DOScale(Vector3.one, animationLength).SetUpdate(true);
        builderRect.gameObject.SetActive(false);
        smallBuilderRect.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }


    public void HideBuilder()
    {
        if (builderRect.gameObject.activeSelf)
        {
            builderRect.DOScale(new Vector3(0.05f, 0.05f), animationLength).
                SetUpdate(true).
                OnComplete(() => gameObject.SetActive(false));
        }
        if (smallBuilderRect.gameObject.activeSelf)
        {
            smallBuilderRect.DOScale(new Vector3(0.05f, 0.05f), animationLength).
            SetUpdate(true).
            OnComplete(() => gameObject.SetActive(false));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        HideBuilder();
    }

    public void Build(int IronCost, int DiamondCost, TowerType towerType = TowerType.none, SupportTowerType supportTowerType = SupportTowerType.none)
    {
        //Debug.Log("Build tower");
        currentTile.BuildTower(towerType, supportTowerType);
        
        repository.Iron -= IronCost;
        repository.Diamond -= DiamondCost;

        HideBuilder();
    }

    public void Upgrade(int totalCost, ResourceType resType)
    {
        if (resType == ResourceType.iron) repository.Iron -= totalCost;
        else if (resType == ResourceType.diamond) repository.Diamond -= totalCost;
        currentTile.UpgradeTower();

        HideBuilder();
    }

    public void Sell(int totalValue, ResourceType resType)
    {
        if (resType == ResourceType.iron) repository.Iron += totalValue;
        else if (resType == ResourceType.diamond) repository.Diamond += totalValue;
        currentTile.FreeTile();

        HideBuilder();
    }
}