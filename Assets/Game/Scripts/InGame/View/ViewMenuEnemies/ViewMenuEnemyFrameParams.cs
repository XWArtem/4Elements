using TMPro;
using UnityEngine;

public class ViewMenuEnemyFrameParams : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI HP, speed, reward;

    public void Init(EnemyBase enemyReference)
    {
        HP.text = $"{enemyReference.HP}";
        speed.text = $"{EnemyBase.EnemySpeedView[enemyReference.EnemySpeed]}";
        reward.text = $"{enemyReference.Reward}";
    }
}
