using UnityEngine;

public class SimpleDILevel : MonoBehaviour
{
    public static SimpleDILevel Instance;

    [SerializeField] private GridSpawner gridSpawner;
    [SerializeField] private InputCameraController inputCameraController;
    [SerializeField] private ControllerGrid controllerGrid;
    [SerializeField] private ControllerBuilderUI controllerBuilderUI;
    [SerializeField] private ResourcesRepository resourcesRepository;
    [SerializeField] private SettingsLevel settingsLevel;
    [SerializeField] private ViewLevelResources viewLevelResources;
    [SerializeField] private Conditions conditions;
    [SerializeField] private ViewLevelTopLayer viewLevelTopLayer;
    [SerializeField] private ControllerLevelScreen controllerLevelScreen;

    public TimeScaleHandler TimeScaleHandler;

    [SerializeField] private DataStorageSingletone dataStorage;

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

        dataStorage.Init();

        TimeScaleHandler = new TimeScaleHandler();
        TimeScaleHandler.SetTimeScale(1f);

        settingsLevel.Init(DataStorageSingletone.Instance.CurrentLevelIndex);

        resourcesRepository.Init(DataStorageSingletone.Instance.CurrentLevelIndex);

        controllerBuilderUI.Init(resourcesRepository);

        viewLevelResources.Init(resourcesRepository);

        controllerGrid.Init(controllerBuilderUI);

        var rand = Random.Range(0, DataStorageGrid.RandomLockedTiles2.Count);

        gridSpawner.Init(DataStorageSingletone.Instance.CurrentLevelIndex,
            DataStorageGrid.RandomLockedTiles2[rand].X,
            DataStorageGrid.RandomLockedTiles2[rand].Y);
        
        inputCameraController.Init(gridSpawner.CellHeight * gridSpawner.LevelDepthTotal);

        conditions.Init();

        viewLevelTopLayer.Init();

        controllerLevelScreen.Init();

        DataStorageSingletone.Instance.CastleHP = 300; // 2sec * 50 fixedUpdates = 100 fixedUpdates. 100*3 = 300
    }
}