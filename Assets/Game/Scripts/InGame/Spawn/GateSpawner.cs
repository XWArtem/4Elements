using System.Collections.Generic;
using UnityEngine;

public class GateSpawner : MonoBehaviour
{
    public static GateSpawner Instance;

    [SerializeField] private GameObject gatePrefab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SpawnGateMultipleEnemies(Transform targetTransform, List<int> numberOfEnemies, List<EnemyType> types, List<PathJoint> path)
    {
        var newGate = Instantiate(gatePrefab);
        newGate.transform.position = targetTransform.position;
        for(int i = 0; i < numberOfEnemies.Count; i++)
        {
            newGate.GetComponent<Gate>().Init(numberOfEnemies[i], types[i], path);
        }
    }

    public void SpawnGateSingleEnemyType(Transform targetTransform, int numberOfEnemies, EnemyType type, List<PathJoint> path)
    {
        var newGate = Instantiate(gatePrefab);
        newGate.transform.position = targetTransform.position;
        newGate.GetComponent<Gate>().Init(numberOfEnemies, type, path);
    }
}