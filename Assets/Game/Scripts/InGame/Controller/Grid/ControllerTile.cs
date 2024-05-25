using DG.Tweening;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class ControllerTile : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private Sprite spriteLocked, spriteAvailable, spriteRoad, spriteIronAvailable, spriteDiamondAvailable;
    [SerializeField] private GameObject fogTransparentSprite;
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private GameObject supportTowerPrefab;

    private SpriteRenderer spriteRenderer;
    public TileBase BaseTile;
    private ControllerTileAnimation animator;

    private IEnumerator stretchingCoroutine;

    private int totalTileCost = 0;
    private int nextGradeCost = 0;
    private ResourceType resType;

    private GameObject tower;
    internal ControllerTower linkedTowerController;

    public int TowerLevel;
    public bool IsSupportTower;

    private bool isActive;

    private void OnEnable()
    {
        Conditions.OnGameOver += (() => isActive = false);
        Conditions.OnGameWin += (() => isActive = false);
    }

    private void OnDisable()
    {
        Conditions.OnGameOver -= (() => isActive = false);
        Conditions.OnGameWin -= (() => isActive = false);
    }

    public void Init(tileType type, bool hiddenInFog, int X, int Y)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();

        isActive = true;

        BaseTile = new TileBase();
        BaseTile.TileType = type;
        BaseTile.HiddenInFog = hiddenInFog;
        BaseTile.X = X;
        BaseTile.Y = Y;

        SetSprite();
        animator = GetComponent<ControllerTileAnimation>();
        animator.Init();

        TowerLevel = 0;
    }

    private void SetSprite()
    {
        fogTransparentSprite.SetActive(BaseTile.HiddenInFog);
        switch (BaseTile.TileType)
        {
            case tileType.locked:
                spriteRenderer.sprite = spriteLocked;
                break;
            case tileType.available:
                spriteRenderer.sprite = spriteAvailable;
                break;
            case tileType.road:
                spriteRenderer.sprite = spriteRoad;
                break;
            case tileType.ironAvailable:
                spriteRenderer.sprite = spriteIronAvailable;
                break;
            case tileType.diamondAvailable:
                spriteRenderer.sprite = spriteDiamondAvailable;
                break;
            default:
                spriteRenderer.sprite = spriteAvailable;
                break;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!isActive) return;

        if (BaseTile.Diggable)
        {
            TryCollectResources();
            AudioManager.Instance.PlayClipByName("04_digging");
            BaseTile.TileType = tileType.road;
            SetSprite();
            ControllerGrid.Instance.DigTile(BaseTile.X, BaseTile.Y);
            ControllerGrid.Instance.UpdateMatrixBackend();
            animator.DigAnimation();
        }
        else if (!BaseTile.Diggable && (BaseTile.TileType == tileType.available || BaseTile.TileType == tileType.ironAvailable || BaseTile.TileType == tileType.diamondAvailable))
        {
            if (!BaseTile.OccupiedByTower)  // go for big builder
            {
                ControllerGrid.Instance.TryOpenBuilder(this, BaseTile.X);
            }
            else  // go for small builder (sell or update)
            {
                ControllerGrid.Instance.TryOpenSmallBuilder(this, totalTileCost, nextGradeCost, resType);
            }
            animator.PointerClickAnimation();
        }
    }

    public void ClearFog()
    {
        BaseTile.HiddenInFog = false;
        SetSprite();
    }

    public void OnPointerUp(PointerEventData eventData)
    {

    }

    public void SetCallToActionAnimation(bool startAnimation)
    {
        if (startAnimation)
        {
            stretchingCoroutine = StretchingAnimation();
            StartCoroutine(stretchingCoroutine);
        }
        spriteRenderer.sortingOrder = startAnimation ? 1 : 0;
    }

    private void TryCollectResources()
    {
        if (BaseTile.TileType == tileType.ironAvailable)
        {
            ResourcesRepository.OnGetIronReward?.Invoke(100);
            AudioManager.Instance.PlayClipByName("05_diamond_metall");
            BaseTile.TileType = tileType.available;
            SetSprite();
            animator.GetIronAnimation();
        }
        else if (BaseTile.TileType == tileType.diamondAvailable)
        {
            ResourcesRepository.OnGetDiamondReward?.Invoke(100);
            AudioManager.Instance.PlayClipByName("05_diamond_metall");
            BaseTile.TileType = tileType.available;
            SetSprite();
            animator.GetDiamondAnimation();
        }
    }

    public void BuildTower(TowerType towerType = TowerType.none, SupportTowerType supportTowerType = SupportTowerType.none)
    {
        TryCollectResources();
        BaseTile.OccupiedByTower = true;
        TowerLevel = 1;
        IsSupportTower = towerType == TowerType.none;

        if (towerType != TowerType.none)
        {
            tower = Instantiate(towerPrefab, Vector2.zero, Quaternion.identity, this.transform);
            Debug.Log("Build " + towerType);
            tower.transform.localPosition = new Vector2(0f, 0.5f);
            linkedTowerController = tower.GetComponent<ControllerTower>();
            linkedTowerController.Init(towerType, 3 + BaseTile.X * 2);
            totalTileCost += DataStorageSingletone.Instance.DataStorageTowers.TowersLvl1.Where(x => x.TowerType == towerType).FirstOrDefault().Cost;
            nextGradeCost = DataStorageSingletone.Instance.DataStorageTowers.TowersLvl2.Where(x => x.TowerType == towerType).FirstOrDefault().Cost;
            resType = DataStorageSingletone.Instance.DataStorageTowers.TowersLvl1.Where(x => x.TowerType == towerType).FirstOrDefault().ResourceType;

            ControllerGrid.OnTowersFieldChanged?.Invoke();
        }
        else
        {
            tower = Instantiate(supportTowerPrefab, Vector2.zero, Quaternion.identity, this.transform);
            Debug.Log("Build " + supportTowerType);
            tower.transform.localPosition = new Vector2(0f, 0.5f);
            tower.GetComponent<ControllerSupportTower>().Init(supportTowerType, BaseTile.X, BaseTile.Y, 3 + BaseTile.X * 2);
            totalTileCost += DataStorageSingletone.Instance.DataStorageTowers.supportTowers.Where(x => x.SupportTowerType == supportTowerType).FirstOrDefault().Cost;
            resType = DataStorageSingletone.Instance.DataStorageTowers.supportTowers.Where(x => x.SupportTowerType == supportTowerType).FirstOrDefault().ResourceType;

            BaseTile.SupportTowerTile = true;
        }

        AudioManager.Instance.PlayClipByName("12_tower_build");
    }

    public void UpgradeTower()
    {
        if (TowerLevel == 1)
        {
            totalTileCost += DataStorageSingletone.Instance.DataStorageTowers.TowersLvl2.
                Where(x => x.TowerType == linkedTowerController.Type).FirstOrDefault().Cost;

            nextGradeCost = DataStorageSingletone.Instance.DataStorageTowers.TowersLvl3.
                Where(x => x.TowerType == linkedTowerController.Type).FirstOrDefault().Cost;
        }
        else if (TowerLevel == 2)
        {
            totalTileCost += DataStorageSingletone.Instance.DataStorageTowers.TowersLvl3.
                Where(x => x.TowerType == linkedTowerController.Type).FirstOrDefault().Cost;

            nextGradeCost = 999999;
        }
        else
        {
            Debug.LogWarning("Wrong tower level!");
            return;
        }
        linkedTowerController.UpdateTowerInfo(++TowerLevel);
    }


    public void FreeTile()
    {
        BaseTile.OccupiedByTower = false;
        BaseTile.SupportTowerTile = false;

        totalTileCost = 0;
        nextGradeCost = 0;
        IsSupportTower = false;
        TowerLevel = 0;

        Destroy(tower);
        linkedTowerController = null;

        ControllerGrid.OnTowersFieldChanged?.Invoke();
    }

    private IEnumerator StretchingAnimation()
    {
        float animLength = 1f;
        while (BaseTile.Diggable)
        {
            transform.DOScale(new Vector2(1.06f, 1.06f), animLength);
            yield return new WaitForSeconds(animLength);
            transform.DOScale(new Vector2(1f, 1f), animLength);
            yield return new WaitForSeconds(animLength);
        }
    }
}