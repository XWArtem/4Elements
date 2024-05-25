using TMPro;
using UnityEngine;

public class ViewLevelResources : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI iron, diamond;

    private ResourcesRepository repository;

    public void Init(ResourcesRepository repository)
    {
        this.repository = repository;
        this.repository.OnResourcesChanged.AddListener(UpdateResourcesInfo);
        UpdateResourcesInfo();
    }

    private void UpdateResourcesInfo()
    {
        iron.text = $"{repository.Iron}";
        diamond.text = $"{repository.Diamond}";
    }
}