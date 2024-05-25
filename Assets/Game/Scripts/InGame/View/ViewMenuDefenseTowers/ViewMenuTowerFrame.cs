using System.Linq;
using TMPro;
using UnityEngine;

public class ViewMenuTowerFrame : MonoBehaviour
{
    [SerializeField] private ViewMenuTowerFrameParams paramsLvl1, paramsLvl2, paramsLvl3;
    [SerializeField] private TowerType type;
    [SerializeField] private TextMeshProUGUI towerName;
    [SerializeField] private TextMeshProUGUI towerDescription;

    public void Init()
    {
        TowerBase tb = DataStorageSingletone.Instance.DataStorageTowers.TowersLvl1.Where(x => x.TowerType == type).FirstOrDefault();
        towerName.text = $"{tb.Name}";
        towerDescription.text = $"{TowerBase.DescriptionView[tb.TowerType]}";
        paramsLvl1.Init(tb, 1);

        tb = DataStorageSingletone.Instance.DataStorageTowers.TowersLvl2.Where(x => x.TowerType == type).FirstOrDefault();
        paramsLvl2.Init(tb, 2);

        tb = DataStorageSingletone.Instance.DataStorageTowers.TowersLvl3.Where(x => x.TowerType == type).FirstOrDefault();
        paramsLvl3.Init(tb, 3);
    }
}