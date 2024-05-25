using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class ControllerEnemy : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hpBarFg;

    private GameObject hpBar;

    private EnemyAnimator enemyAnimator;

    private List<PathJoint> currentPath;
    private int nextJointIndex;

    private Vector2 targetJointPos;

    private float maxStep;

    private bool isMoving = true;
    private bool isDamagingCastle;

    private EnemyType enemyType;
    private EnemyBase enemyBase;

    private float maxHP;
    public float currentHP;

    public bool IsDead;

    private float velocityMultiplier = 1f;


    public void Init(List<PathJoint> path, EnemyType enemyType, int orderInLayer)
    {
        enemyAnimator = GetComponent<EnemyAnimator>();
        hpBar = hpBarFg.transform.parent.gameObject;
        enemyAnimator.Init(orderInLayer, hpBar.transform);

        this.enemyType = enemyType;
        enemyBase = new EnemyBase(
            DataStorageSingletone.Instance.DataStorageEnemies.Enemies.FirstOrDefault(x => x.EnemyType == this.enemyType).Name,
            DataStorageSingletone.Instance.DataStorageEnemies.Enemies.FirstOrDefault(x => x.EnemyType == this.enemyType).HP,
            DataStorageSingletone.Instance.DataStorageEnemies.Enemies.FirstOrDefault(x => x.EnemyType == this.enemyType).EnemySpeed,
            DataStorageSingletone.Instance.DataStorageEnemies.Enemies.FirstOrDefault(x => x.EnemyType == this.enemyType).EnemyType,
            DataStorageSingletone.Instance.DataStorageEnemies.Enemies.FirstOrDefault(x => x.EnemyType == this.enemyType).Reward
            );
        
        maxHP = enemyBase.HP;
        currentHP = maxHP;

        maxStep = DataStorageSingletone.Instance.DataStorageEnemies.EnemySpeedToMaxStep[enemyBase.EnemySpeed];
        Debug.Log("Max step for " + enemyBase.EnemyType + " is : " + maxStep);

        currentPath = path;
        nextJointIndex = currentPath.Count - 1;
        targetJointPos = new Vector2(currentPath[nextJointIndex].X, currentPath[nextJointIndex].Y);

        isDamagingCastle = false;

        Conditions.OnGameOver += DeactivateEnemy;
    }

    private void OnDisable()
    {
        Conditions.OnGameOver -= DeactivateEnemy;
    }

    private void FixedUpdate()
    {
        if (!isMoving && isDamagingCastle)
        {
            DataStorageSingletone.Instance.CastleHP--;
        }
        if (!isMoving) return;

        //if (Mathf.Approximately(transform.position.x, currentPath[nextJointIndex].X) &&
        //    Mathf.Approximately(transform.position.y, currentPath[nextJointIndex].Y))
        if (Mathf.Abs(transform.position.x - currentPath[nextJointIndex].X) < 0.2f &&
            Mathf.Abs(transform.position.y - currentPath[nextJointIndex].Y) < 0.2f)
        {
            nextJointIndex--;
            if (nextJointIndex < 0)
            {
                isMoving = false;
                isDamagingCastle = true;
                return;
            }
            targetJointPos = new Vector2(currentPath[nextJointIndex].X, currentPath[nextJointIndex].Y);
            CheckOutlookDirection();
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, targetJointPos, maxStep * velocityMultiplier);
        }
    }

    private void CheckOutlookDirection()
    {
        if (targetJointPos.x - transform.position.x < -1f && enemyAnimator.CurrentDirection != EnemyOutlookDirection.left)
        {
            enemyAnimator.CurrentDirection = EnemyOutlookDirection.left;
        }
        else if (targetJointPos.x - transform.position.x > 1f && enemyAnimator.CurrentDirection != EnemyOutlookDirection.right)
        {
            enemyAnimator.CurrentDirection = EnemyOutlookDirection.right;
        }
        else if (targetJointPos.y - transform.position.y > 1f && enemyAnimator.CurrentDirection != EnemyOutlookDirection.back)
        {
            enemyAnimator.CurrentDirection = EnemyOutlookDirection.back;
        }
    }

    private void DeactivateEnemy()
    {
        isMoving = false;
        isDamagingCastle = false;
    }

    public void TakeDamage(float dmg, AttackSpecialEffect attackSpecialEffect)
    {
        currentHP = Mathf.Max(0, currentHP - dmg);
        hpBarFg.size = new Vector2(Mathf.Max(0, 0.41f * (currentHP / maxHP)), 0.07f);
        CheckDeath(attackSpecialEffect);
    }

    private void CheckDeath(AttackSpecialEffect attackSpecialEffect)
    {
        if (IsDead) return;

        if (currentHP <= 0)
        {
            isMoving = false;
            IsDead = true;
            isDamagingCastle = false;
            hpBar.GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
            enemyAnimator.OnDeathAnimation(attackSpecialEffect);

            // get reward
            ResourcesRepository.OnGetIronReward?.Invoke(enemyBase.Reward);

            // register death
            Conditions.OnEnemyDie?.Invoke();
        }
    }

    public void ApplySpecialEffect(AttackSpecialEffect effect, int effectLevel)
    {
        switch(effect)
        {
            case AttackSpecialEffect.slow:
                if (effectLevel == 1 && velocityMultiplier > 0.75f) velocityMultiplier = 0.75f;
                if (effectLevel == 2 && velocityMultiplier > 0.65f) velocityMultiplier = 0.65f;
                if (effectLevel == 3) velocityMultiplier = 0.6f;
                enemyAnimator.ApplyFreezeEffect();
                break;
            case AttackSpecialEffect.AoE:
                //enemyAnimator.ApplyFreezeEffect();
                break;
        }
    }
}