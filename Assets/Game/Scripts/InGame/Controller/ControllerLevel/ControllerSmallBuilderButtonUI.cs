using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum UtilityButtonType
{
    upgrade,
    sell
}


public class ControllerSmallBuilderButtonUI : MonoBehaviour
{
    //[SerializeField] private UtilityButtonType buttonType;
    ////[SerializeField] private int costIron;
    ////[SerializeField] private int cost;

    //[SerializeField] private Image resourceImage;

    //[SerializeField] private TextMeshProUGUI costText;

    //[SerializeField] private Sprite ironSprite, diamondSprite;

    //private Button button;

    //private ResourcesRepository repository;
    //private ControllerBuilderUI controllerBuilderUI;

    //private int totalCost;
    //private ResourceType resourcesType;

    //public void Init(ControllerBuilderUI controllerBuilderUI, ResourcesRepository repo)
    //{
    //    button = GetComponent<Button>();

    //    repository = repo;
    //    this.controllerBuilderUI = controllerBuilderUI;

    //    if (buttonType == UtilityButtonType.upgrade)
    //    {
    //        button.onClick.AddListener(TryUpgrade);
    //    }
    //    else if (buttonType == UtilityButtonType.sell)
    //    {
    //        button.onClick.AddListener(TrySell);
    //    }
    //}

    //private void OnDestroy()
    //{
    //    button?.onClick.RemoveAllListeners();
    //}

    //public void UpdateView(int totalCost, ResourceType resourcesType)
    //{
    //    if (buttonType == UtilityButtonType.sell)
    //    {
    //        button.interactable = true;
    //        costText.text = $"{totalCost}";
    //        resourceImage.sprite = (resourcesType == ResourceType.iron) ? ironSprite : diamondSprite;
    //    }
    //    else if (buttonType == UtilityButtonType.upgrade)
    //    {
    //        button.interactable = true;
    //        resourceImage.sprite = (resourcesType == ResourceType.iron) ? ironSprite : diamondSprite;
    //    }
    //}

    //public void UpdateSellLogic(int totalCost, ResourceType resourcesType)
    //{
    //    this.totalCost = totalCost;
    //    this.resourcesType = resourcesType;
    //}

    //public void UpdateUpgradeLogic(int towerLevel, int nextGradeCost, ResourceType resourcesType)
    //{
    //    if (towerLevel == 3)
    //    {

    //    }
    //    this.totalCost = nextGradeCost;
    //    this.resourcesType = resourcesType;
    //}

    //private void TryUpgrade()
    //{
    //    controllerBuilderUI.Upgrade(totalCost, resourcesType);
    //    //if ()
    //    //if ((repository.Iron >= costIron && repository.Diamond >= costDiamond)
    //    //    {
    //    //    controllerBuilderUI.Build(costIron, costDiamond, towerType, supportTowerType);

    //    //}
    //}

    //private void TrySell()
    //{
    //    controllerBuilderUI.Sell(totalCost, resourcesType);
    //}
}
