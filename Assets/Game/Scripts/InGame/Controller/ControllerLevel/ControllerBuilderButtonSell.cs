using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControllerBuilderButtonSell : MonoBehaviour
{
    [SerializeField] private Image resourceImage;

    [SerializeField] private TextMeshProUGUI costText;

    [SerializeField] private Sprite ironSprite, diamondSprite;

    private Button button;

    private ResourcesRepository repository;
    private ControllerBuilderUI controllerBuilderUI;

    private int valueToSell;
    private ResourceType resourcesType;

    private readonly Color colorAvailableCost = new Color(250f / 255f, 227f / 255f, 75f / 255f, 1f);
    private readonly Color colorUnavailableCost = new Color(250f / 255f, 227f / 255f, 75f / 255f, 0.5f);

    public void Init(ControllerBuilderUI controllerBuilderUI)
    {
        button = GetComponent<Button>();

        this.controllerBuilderUI = controllerBuilderUI;

        button.onClick.AddListener(TrySell);
    }

    private void OnDestroy()
    {
        button?.onClick.RemoveAllListeners();
    }

    public void UpdateButton(int totalValue, ResourceType resourcesType)
    {
        valueToSell = totalValue/2;
        this.resourcesType = resourcesType;

        button.interactable = true;
        costText.text = $"{valueToSell}";
        resourceImage.sprite = (resourcesType == ResourceType.iron) ? ironSprite : diamondSprite;
    }

    private void TrySell()
    {
        controllerBuilderUI.Sell(valueToSell, resourcesType);
        AudioManager.Instance.PlayTapSound();
    }
}
