using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBuilderButtonUI : MonoBehaviour
{
    [SerializeField] private TowerType towerType;
    [SerializeField] private SupportTowerType supportTowerType;
    [SerializeField] private int costIron;
    [SerializeField] private int costDiamond;

    [SerializeField] private TextMeshProUGUI costText;
    
    //[SerializeField] private ResourceType resourceType;

    private Button buttonBuild;
    private ResourcesRepository repository;
    private ControllerBuilderUI controllerBuilderUI;

    private readonly Color colorAvailableCost = new Color(250f / 255f, 227f / 255f, 75f / 255f, 1f);
    private readonly Color colorUnavailableCost = new Color(250f / 255f, 227f / 255f, 75f / 255f, 0.5f);

    public void Init(ControllerBuilderUI controllerBuilderUI, ResourcesRepository repo)
    {
        buttonBuild = GetComponent<Button>();

        repository = repo;
        this.controllerBuilderUI = controllerBuilderUI;

        buttonBuild.onClick.AddListener(TryBuild);
    }

    private void OnDestroy()
    {
        buttonBuild?.onClick.RemoveAllListeners();
    }

    public void UpdateView()
    {
        buttonBuild.interactable = IsAvailable();
        costText.color = IsAvailable() ? colorAvailableCost : colorUnavailableCost;
    }

    private bool IsAvailable()
    {
        return (repository.Iron >= costIron && repository.Diamond >= costDiamond);
    }

    private void TryBuild()
    {
        if (IsAvailable())
        {
            controllerBuilderUI.Build(costIron, costDiamond, towerType, supportTowerType);
            AudioManager.Instance.PlayTapSound();
        }
    }
}
