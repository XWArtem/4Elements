using System.Linq;
using TMPro;
using UnityEngine;

public class ViewMenuSupportTowerFrame : MonoBehaviour
{
    [SerializeField] private ViewMenuTowerFrameParams paramsCommon;
    [SerializeField] private SupportTowerType type;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerDescription;

    public void Init()
    {
        SupportTowerBase stb = DataStorageSingletone.Instance.DataStorageTowers.supportTowers.Where(x => x.SupportTowerType == type).FirstOrDefault();
        towerName.text = $"{stb.Name}";
        towerDescription.text = $"{SupportTowerBase.DescriptionView[stb.SupportTowerType]}";
        //paramsCommon.Init(stb, 1);
    }
}
