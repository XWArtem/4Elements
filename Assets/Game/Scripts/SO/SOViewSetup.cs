using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOViewSetup : ScriptableObject
{
    public string prefabName;

    public int numberOfPrefabsToCreate;
    public Vector3[] spawnPoints;

    public float ScreenTransitionTime;
}
