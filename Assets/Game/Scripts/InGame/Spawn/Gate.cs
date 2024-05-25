using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject zombiePrefab;
    [SerializeField] private GameObject ogrePrefab;
    [SerializeField] private GameObject zombieOgrePrefab;
    [SerializeField] private GameObject batPrefab;
    [SerializeField] private GameObject spiderPrefab;
    [SerializeField] private GameObject fireGolemPrefab;
    [SerializeField] private GameObject robotPrefab;

    [SerializeField] private SpriteRenderer shine;

    private Dictionary<EnemyType, GameObject> enemyTypes;

    private float SpawnDelayMin => SettingsLevel.EnemySpawnDelayMin;
    private float SpawnDelayMax => SettingsLevel.EnemySpawnDelayMax;

    private int enemiesLeftToSpawn = 0;

    public void Init(int numberOfEnemies, EnemyType type, List<PathJoint> path)
    {
        enemyTypes = new Dictionary<EnemyType, GameObject>()
        {
            {EnemyType.zombie, zombiePrefab},
            {EnemyType.ogre, ogrePrefab},
            {EnemyType.zombieOgre, zombieOgrePrefab},
            {EnemyType.bat, batPrefab},
            {EnemyType.spider, spiderPrefab},
            {EnemyType.fireGolem, fireGolemPrefab},
            {EnemyType.robot, robotPrefab}
        };

        enemiesLeftToSpawn = numberOfEnemies;

        int counter = 0;
        while (counter++ < numberOfEnemies)
        {
            var layerOrder = 5 + counter;
            this.ActionDelayed(Random.Range(SpawnDelayMin, SpawnDelayMax) * (counter - 1), () => SpawnEntity(type, path, layerOrder));
        }
        //this.ActionDelayed(SpawnDelayMax * counter, () => GetComponent<SpriteRenderer>().DOColor(new Color(186f / 255f, 186f / 255f, 186f / 255f, 0.8f), 2f));
        shine.color = new Color(1f, 1f, 1f, 0f);

        AudioManager.Instance.PlayClipByName("06_start_wave");
    }

    private void SpawnEntity(EnemyType type, List<PathJoint> path, int orderInLayer)
    {
        ShineOn();

        var newEnemy = Instantiate(enemyTypes[type]);
        newEnemy.transform.SetParent(this.transform);
        newEnemy.transform.localPosition = new Vector2(Random.Range(-0.25f, 0.25f), Random.Range(-0.02f, 0.02f));

        newEnemy.GetComponent<ControllerEnemy>().Init(path, type, orderInLayer);
        enemiesLeftToSpawn--;
        if (enemiesLeftToSpawn == 0)
        {
            GetComponent<SpriteRenderer>().DOColor(new Color(186f / 255f, 186f / 255f, 186f / 255f, 0.8f), 2f);
        }
    }

    private void ShineOn()
    {
        shine.DOFade(0.3f, 0.1f).OnComplete(ShineOff);
    }

    private void ShineOff()
    {
        shine.DOFade(0f, 0.2f);
    }
}