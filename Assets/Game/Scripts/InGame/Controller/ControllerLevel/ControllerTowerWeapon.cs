using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class ControllerTowerWeapon : MonoBehaviour
{
    [SerializeField] private List<GameObject> projectilePool;
    [SerializeField] private Sprite fireProjectileSprite, waterProjectileSprite, airProjectileSprite, earthProjectileSprite;
    [SerializeField] private List<Sprite> explosionSprites;

    private SpriteRenderer spriteRenderer;
    private int projectileIndex = 0;
    private float baseProjectileFlightDuration;
    private AttackSpecialEffect specialEffect;
    private int parentTowerLevel;

    private bool isShooting;

    private float attackDelay;
    private float damageMin;
    private float damageMax;

    private float attackPowerMultiplier = 1f;
    private float attackSpeedMultiplier = 1f;

    private TowerType parentTowerType;
    

    private IEnumerator shootRoutine;

    private List<CircleCollider2D> collidersEarthProjectiles = new List<CircleCollider2D>();

    public List<ControllerEnemy> EnemiesInView = new List<ControllerEnemy>();

    public void Init(TowerType towerType, AttackSpecialEffect specialEffect)
    {
        foreach (GameObject p in projectilePool)
        {
            p.SetActive(false);
            spriteRenderer = p.GetComponent<SpriteRenderer>();
            parentTowerType = towerType;
            switch (towerType)
            {
                case TowerType.fire:
                    spriteRenderer.sprite = fireProjectileSprite;
                    break;
                case TowerType.water:
                    spriteRenderer.sprite = waterProjectileSprite;
                    break;
                case TowerType.air:
                    spriteRenderer.sprite = airProjectileSprite;
                    break;
                case TowerType.earth:
                    spriteRenderer.sprite = earthProjectileSprite;
                    var collider = p.AddComponent<CircleCollider2D>();
                    collider.isTrigger = true;
                    collidersEarthProjectiles.Add(collider);
                    p.AddComponent<ColliderProjectileAoE>();
                    p.AddComponent<Rigidbody2D>();
                    p.GetComponent<Rigidbody2D>().gravityScale = 0f;
                    p.GetComponent<ColliderProjectileAoE>().Init(explosionSprites, earthProjectileSprite);
                    break;
            }
        }

        baseProjectileFlightDuration = DataStorageSingletone.Instance.DataStorageTowers.TowerProjectileFlightTimeBase[towerType];
        this.specialEffect = specialEffect;

        isShooting = false;
    }

    public void UpdateParentTowerLevel(int newLevel)
    {
        parentTowerLevel = newLevel;
        if (collidersEarthProjectiles.Count != 0)
        {
            foreach (CircleCollider2D collider in collidersEarthProjectiles)
            {
                if (newLevel == 1) collider.radius = 1f;
                if (newLevel == 2) collider.radius = 1.25f;
                if (newLevel == 3) collider.radius = 1.35f;
            }
        }
    }

    public void CheckAttackState()
    {
        if (!isShooting && EnemiesInView.Count > 0)
        {
            isShooting = true;
            shootRoutine = ShootRoutine();
            StartCoroutine(shootRoutine);
        }
        else if (isShooting && EnemiesInView.Count == 0 && shootRoutine != null)
        {
            isShooting = false;
            StopCoroutine(shootRoutine);
        }
    }

    public void UpdateCharacteristics(float attackDelay, float dmgMin, float dmgMax)
    {
        this.attackDelay = attackDelay;
        damageMin = dmgMin;
        damageMax = dmgMax;
    }

    public void SetSupportInjection(SupportTowerType injectionType, bool inject)
    {
        switch (injectionType)
        {
            case SupportTowerType.speed:
                attackSpeedMultiplier *= inject ? .66666f : 1.5f;
                break;
            case SupportTowerType.power:
                attackPowerMultiplier *= inject ? 1.5f : .666666f;
                break;
        }
    }

    private IEnumerator ShootRoutine()
    {
        while (isShooting)
        {
            CheckAttackState();
            RecalculateQueue();
            float dmg = Random.Range(damageMin, damageMax) * attackPowerMultiplier;

            if (EnemiesInView.Count > 0) PerformAttack(EnemiesInView[0], dmg); // sometimes provokes errors without 'if' statement

            if (specialEffect == AttackSpecialEffect.doubleShot && EnemiesInView.Count > 1) // double shot
            {
                CheckAttackState();
                RecalculateQueue();
                PerformAttack(EnemiesInView[1], dmg);
            }
            
            yield return new WaitForSeconds(attackDelay * attackSpeedMultiplier);
            CheckAttackState();
            RecalculateQueue();
        }
    }

    private void RecalculateQueue()
    {
        for (int j = 0; j < EnemiesInView.Count; j++)
        {
            if (EnemiesInView[j].IsDead)
            {
                EnemiesInView.RemoveAt(j);
                j--;
            }
        }

        if (EnemiesInView.Count <= 0)
        {
            CheckAttackState();
            return;
        }
    }

    private void PerformAttack(ControllerEnemy enemy, float dmg)
    {
        LaunchProjectile(dmg, enemy);
    }

    public void LaunchProjectile(float damage, ControllerEnemy target)
    {
        projectilePool[projectileIndex].transform.localPosition = new Vector2(0f, 0.3f);

        Vector3 vectorToTarget = target.transform.position - projectilePool[projectileIndex].transform.position;
        Vector3 rotatedVectorToTarget = Quaternion.Euler(0, 0, 90) * vectorToTarget;

        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: rotatedVectorToTarget);
        projectilePool[projectileIndex].transform.rotation = targetRotation;

        //var projectileAcceleration = Mathf.Sqrt(Vector2.Dot(transform.position, target.transform.position)); // provokes ERRORS
        //var projectileAcceleration = 1f;

        //Debug.Log("projectileAcceleration is: " + projectileAcceleration);

        projectilePool[projectileIndex].SetActive(true);
        if (specialEffect == AttackSpecialEffect.AoE)
        {
            projectilePool[projectileIndex].GetComponent<ColliderProjectileAoE>().Launch(damage);
        }
        int index = projectileIndex;
        projectilePool[projectileIndex].transform.DOMove(target.transform.position, baseProjectileFlightDuration * .6f).
            SetEase(Ease.InSine).
            OnComplete(() => ProjectileReachTarget(damage, target, index));

        projectileIndex++;
        if (projectileIndex >= projectilePool.Count) projectileIndex = 0;

        // shoot sound
        switch (parentTowerType)
        {
            case TowerType.fire:
                AudioManager.Instance.PlayClipByName("08_fire_tower");
                break;
            case TowerType.water:
                AudioManager.Instance.PlayClipByName("11_water_tower");
                break;
            case TowerType.earth:
                AudioManager.Instance.PlayClipByName("10_earth_tower");
                break;
            case TowerType.air:
                AudioManager.Instance.PlayClipByName("09_air_tower");
                break;
        }
    }

    private void ProjectileReachTarget(float damage, ControllerEnemy target, int projectileIndex)
    {
        if (specialEffect == AttackSpecialEffect.AoE)
        {
            projectilePool[projectileIndex].GetComponent<ColliderProjectileAoE>().OnApplyDamage();
            //projectilePool[projectileIndex].SetActive(false);

            target.ApplySpecialEffect(specialEffect, parentTowerLevel);

            return;
        }

        target.TakeDamage(damage, specialEffect);
        target.ApplySpecialEffect(specialEffect, parentTowerLevel);

        projectilePool[projectileIndex].SetActive(false);
    }
}
