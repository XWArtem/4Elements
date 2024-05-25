using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBuilderButtonUpgrade : MonoBehaviour
{
    [SerializeField] private Image resourceImage;

    [SerializeField] private TextMeshProUGUI costText;

    [SerializeField] private Sprite ironSprite, diamondSprite;

    private Button button;

    private ResourcesRepository repository;
    private ControllerBuilderUI controllerBuilderUI;

    private int totalCost;
    private ResourceType resourcesType;

    private readonly Color colorAvailableCost = new Color(250f / 255f, 227f / 255f, 75f / 255f, 1f);
    private readonly Color colorUnavailableCost = new Color(250f / 255f, 227f / 255f, 75f / 255f, 0.5f);

    public void Init(ControllerBuilderUI controllerBuilderUI, ResourcesRepository repo)
    {
        button = GetComponent<Button>();

        repository = repo;
        this.controllerBuilderUI = controllerBuilderUI;
        button.onClick.AddListener(TryUpgrade);
    }

    private void OnDestroy()
    {
        button?.onClick.RemoveAllListeners();
    }

    public void UpdateButton(bool isUpgradable, int towerLevel, int nextGradeCost, ResourceType resourcesType)
    {
        if (!isUpgradable || towerLevel == 3)
        {
            gameObject.SetActive(false);
            return;
            //button.interactable = false;
            //this.totalCost = 0;
            //costText.text = $"{totalCost}";
        }
        gameObject.SetActive(true);
        this.totalCost = nextGradeCost;
        this.resourcesType = resourcesType;
        costText.text = $"{totalCost}";
        button.interactable = (resourcesType == ResourceType.iron) ? repository.Iron >= nextGradeCost : repository.Diamond >= nextGradeCost;
        resourceImage.sprite = (resourcesType == ResourceType.iron) ? ironSprite : diamondSprite;
    }

    private void TryUpgrade()
    {
        controllerBuilderUI.Upgrade(totalCost, resourcesType);
        AudioManager.Instance.PlayTapSound();
    }
}
