using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ControllerTower : MonoBehaviour
{
    [SerializeField] private List<Sprite> fireTowerSprites;
    [SerializeField] private List<Sprite> airTowerSprites;
    [SerializeField] private List<Sprite> waterTowerSprites;
    [SerializeField] private List<Sprite> earthTowerSprites;

    [SerializeField] private CircleCollider2D triggerZone;

    [SerializeField] private SpriteRenderer visualGradeOne, visualGradeTwo, visualGradeThree;
    [SerializeField] private Sprite visualGradeActive, visualGradeInactive;

    private ControllerTowerWeapon weapon;

    private SpriteRenderer spriteRenderer;

    private int towerLevel;
    internal TowerType Type;

    private float animationLength = 0.3f;
    private TowerBase towerBase;

    private const string EnemyTag = "Enemy";

    private float attackDistanceMultiplier = 1f;

    public void Init(TowerType type, int orderInLayer)
    {
        transform.localScale = new Vector2(0.6f, 0.6f);
        towerLevel = 1;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sortingOrder = orderInLayer;
        visualGradeOne.sortingOrder = orderInLayer;
        visualGradeTwo.sortingOrder = orderInLayer;
        visualGradeThree.sortingOrder = orderInLayer;

        Type = type;

        weapon = GetComponent<ControllerTowerWeapon>();
        UpdateTowerInfo(towerLevel);

        weapon.Init(towerBase.TowerType, towerBase.AttackSpecialEffect);
        weapon.UpdateParentTowerLevel(towerLevel);

        //UpdateSprite();
        //UpdateTowerCharacteristics();
    }

    public void UpdateTowerInfo(int newTowerLevel)
    {
        towerLevel = newTowerLevel;
        switch (towerLevel)
        {
            case 1:
                towerBase = DataStorageSingletone.Instance.DataStorageTowers.TowersLvl1.FirstOrDefault(x => x.TowerType == Type);
                TowerPlacementAnimation();
                break;
            case 2:
                towerBase = DataStorageSingletone.Instance.DataStorageTowers.TowersLvl2.FirstOrDefault(x => x.TowerType == Type);
                TowerUpdateAnimation();
                weapon.UpdateParentTowerLevel(newTowerLevel);
                break;
            case 3:
                towerBase = DataStorageSingletone.Instance.DataStorageTowers.TowersLvl3.FirstOrDefault(x => x.TowerType == Type);
                TowerUpdateAnimation();
                weapon.UpdateParentTowerLevel(newTowerLevel);
                break;
        }

        UpdateSprite();
        UpdateTowerCharacteristics();
    }

    private void UpdateTowerCharacteristics()
    {
        triggerZone.radius = DataStorageSingletone.Instance.DataStorageTowers.TowerDistanceValue[towerBase.AttackDistanceType] * attackDistanceMultiplier;

        weapon.UpdateCharacteristics(
            DataStorageSingletone.Instance.DataStorageTowers.TowerAttackDelay[towerBase.FireRate],
            towerBase.DamageMin,
            towerBase.DamageMax);
    }

    public void SetSupportInjection(SupportTowerType injectionType, bool inject)
    {
        if (injectionType == SupportTowerType.distance)
        {
            attackDistanceMultiplier *= inject ? 1.5f : .666666f;
            UpdateTowerCharacteristics();
        }
        else
        {
            weapon?.SetSupportInjection(injectionType, inject);
        }
    }

    private void UpdateSprite()
    {
        switch (Type)
        {
            case TowerType.fire:
                spriteRenderer.sprite = fireTowerSprites[towerLevel - 1];
                break;
            case TowerType.air:
                spriteRenderer.sprite = airTowerSprites[towerLevel - 1];
                break;
            case TowerType.water:
                spriteRenderer.sprite = waterTowerSprites[towerLevel - 1];
                break;
            case TowerType.earth:
                spriteRenderer.sprite = earthTowerSprites[towerLevel - 1];
                break;
            default:
                Debug.LogWarning("Towertype not defined!");
                break;
        }
        visualGradeOne.sprite = visualGradeActive;
        visualGradeTwo.sprite = towerLevel >= 2 ? visualGradeActive : visualGradeInactive;
        visualGradeThree.sprite = towerLevel >= 3 ? visualGradeActive : visualGradeInactive;
    }

    private void TowerPlacementAnimation()
    {
        transform.DOScale(Vector2.one, animationLength).
            SetUpdate(true).
            SetEase(Ease.OutElastic);
    }

    private void TowerUpdateAnimation()
    {
        transform.DOScale(new Vector2(1.2f, 1.2f), animationLength).
            SetEase(Ease.OutElastic).
            OnComplete(() => transform.DOScale(Vector2.one, animationLength).
            SetUpdate(true).
            SetEase(Ease.OutElastic));
        AudioManager.Instance.PlayClipByName("15_challange");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(EnemyTag))
        {
            //Debug.Log("Enemy is close");
            if (!weapon.EnemiesInView.Contains(collision.GetComponent<ControllerEnemy>()))
            {
                weapon.EnemiesInView.Add(collision.GetComponent<ControllerEnemy>());
            }
            weapon.CheckAttackState();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(EnemyTag))
        {
            //Debug.Log("Enemy is far");
            if (weapon.EnemiesInView.Contains(collision.GetComponent<ControllerEnemy>()))
            {
                weapon.EnemiesInView.Remove(collision.GetComponent<ControllerEnemy>());
            }
            weapon.CheckAttackState();
        }
    }
}