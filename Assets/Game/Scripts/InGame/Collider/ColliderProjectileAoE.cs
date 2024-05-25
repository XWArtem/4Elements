using System.Collections.Generic;
using UnityEngine;

public class ColliderProjectileAoE : MonoBehaviour
{
    private List<Sprite> explosionSprites = new List<Sprite>();
    private Sprite projectileSprite;
    private List<ControllerEnemy> enemies = new List<ControllerEnemy>();
    private float damage;
    private const string EnemyTag = "Enemy";
    private SpriteRenderer spriteRenderer;

    public void Init(List<Sprite> explosionSprites, Sprite projectileSprite)
    {
        this.explosionSprites = explosionSprites;
        this.projectileSprite = projectileSprite;
    }

    public void Launch(float damage)
    {
        spriteRenderer.sprite = projectileSprite;
        this.damage = damage;
    }

    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnApplyDamage()
    {
        foreach (var enemy in enemies) 
        {
            enemy.TakeDamage(Random.Range(damage * 0.5f, damage), AttackSpecialEffect.AoE);
        }
        int index = 0;
        while (index < explosionSprites.Count)
        {
            var tempIndex = index;
            this.ActionDelayed(0.1f * tempIndex, () => spriteRenderer.sprite = explosionSprites[tempIndex]);
            index++;
        }
        this.ActionDelayed(0.1f * index, () => gameObject.SetActive(false));
        this.ActionDelayed(0.1f * index, () => spriteRenderer.sprite = projectileSprite);


        //this.ActionDelayed
    }

    private void OnDisable()
    {
        enemies.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(EnemyTag))
        {
            enemies.Add(collision.GetComponent<ControllerEnemy>());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(EnemyTag) && enemies.Contains(collision.GetComponent<ControllerEnemy>()))
        {
            enemies.Add(collision.GetComponent<ControllerEnemy>());
        }
    }
}
