using System.Linq;
using TMPro;
using UnityEngine;

public class ViewMenuEnemyFrame : MonoBehaviour
{
    [SerializeField] private ViewMenuEnemyFrameParams enemyParams;
    [SerializeField] private EnemyType type;
    [SerializeField] private TextMeshProUGUI enemyName;
    [SerializeField] private TextMeshProUGUI enemyDescription;

    public void Init()
    {
        EnemyBase eb = DataStorageSingletone.Instance.DataStorageEnemies.Enemies.Where(x => x.EnemyType == type).First();
        enemyName.text = $"{eb.Name}";
        enemyDescription.text = $"{EnemyBase.DescriptionView[eb.EnemyType]}";
        enemyParams.Init(eb);
    }
}
