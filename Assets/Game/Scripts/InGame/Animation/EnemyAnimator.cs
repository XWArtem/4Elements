using System.Collections;
using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    [SerializeField] private Sprite[] spritesBack;
    [SerializeField] private Sprite[] spritesFront;
    [SerializeField] private Sprite[] spritesRight;

    [SerializeField] private Sprite[] spritesDeathOnExplosion;
    [SerializeField] private Sprite[] spritesDeathSimple;

    private SpriteRenderer spriteRenderer;

    private IEnumerator PlayAnimationRoutine;
    private WaitForSeconds tick = new WaitForSeconds(0.15f);
    private EnemyOutlookDirection currentDirection;
    public EnemyOutlookDirection CurrentDirection
    {
        get { return currentDirection; }
        set
        {
            currentDirection = value;
            SetNewDirection(currentDirection);
        }
    }

    private Transform hpBar;

    public void Init(int orderInLayer, Transform hpBar)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        this.hpBar = hpBar;
        spriteRenderer.sortingOrder = 5 + orderInLayer;

        CurrentDirection = EnemyOutlookDirection.back;
    }

    public void SetNewDirection(EnemyOutlookDirection targetDirection)
    {
        currentDirection = targetDirection;

        // flip if needed
        if (currentDirection == EnemyOutlookDirection.left)
        {
            transform.localScale = new Vector2(-1f, 1f);
            hpBar.transform.localScale = new Vector2(-1f, 1f);
        }
        if (currentDirection == EnemyOutlookDirection.right)
        {
            transform.localScale = new Vector2(1f, 1f);
            hpBar.transform.localScale = new Vector2(1f, 1f);
        }

        // start animation
        if (PlayAnimationRoutine != null)
        {
            StopCoroutine(PlayAnimationRoutine);
        }
        switch (currentDirection)
        {
            case EnemyOutlookDirection.back:
                PlayAnimationRoutine = PlayAnimation(spritesBack);
                break;
            case EnemyOutlookDirection.right:
                PlayAnimationRoutine = PlayAnimation(spritesRight);
                break;
            case EnemyOutlookDirection.left:
                PlayAnimationRoutine = PlayAnimation(spritesRight);
                break;
        }

        StartCoroutine(PlayAnimationRoutine);
    }

    private IEnumerator PlayAnimation(Sprite[] sprites, bool repeat = true)
    {
        int i = 0;
        if (!repeat)
        {
            while (i < sprites.Length)
            {
                spriteRenderer.sprite = sprites[i++];
                yield return tick;
            }
            spriteRenderer.enabled = false;
            yield break;
        }

        while (true)
        {
            spriteRenderer.sprite = sprites[i++];
            yield return tick;
            if (i > sprites.Length - 1) { i = 0; }
        }
    }

    public void OnDeathAnimation(AttackSpecialEffect attackSpecialEffect)
    {
        StopCoroutine(PlayAnimationRoutine);

        if (attackSpecialEffect == AttackSpecialEffect.AoE)
        {
            PlayAnimationRoutine = PlayAnimation(spritesDeathOnExplosion, false);
        }
        else
        {
            PlayAnimationRoutine = PlayAnimation(spritesDeathSimple, false);
        }
        
        StartCoroutine(PlayAnimationRoutine);
        //transform.DOLocalMoveY(transform.localPosition.y + 0.5f, 1f);
        //spriteRenderer.color(0f, 1f);
    }

    public void ApplyFreezeEffect()
    {
        spriteRenderer.color = new Color(110f/255f, 191f/255f, 1f, spriteRenderer.color.a);
    }

    private void ApplyAoEVisualEffect()
    {

    }
}