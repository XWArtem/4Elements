using TMPro;
using UnityEngine;

public class ViewMenuTowerFrameParams : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelIndex, damageRange, attackRange, fireRate, cost;

    public void Init(TowerBase towerReference, int lvlIndex)
    {
        levelIndex.text = $"{lvlIndex}";
        damageRange.text = $"{towerReference.DamageMin}-{towerReference.DamageMax}";
        attackRange.text = $"{TowerBase.AttackDistanceView[towerReference.AttackDistanceType]}";
        fireRate.text = $"{TowerBase.FireRateView[towerReference.FireRate]}";
        cost.text = $"{towerReference.Cost}";
    }
}
