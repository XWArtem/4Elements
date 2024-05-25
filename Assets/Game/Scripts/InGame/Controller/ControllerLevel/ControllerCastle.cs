using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCastle : MonoBehaviour
{
    [SerializeField] private List<Sprite> explosionSpriteSequence;
    [SerializeField] private SpriteRenderer explosionSpriteRenderer;

    private bool isDestroyed;

    private void OnEnable()
    {
        explosionSpriteRenderer.gameObject.SetActive(false);
        isDestroyed = false;
        Conditions.OnCastleDamaged += CastleDamagedAnimation;
    }

    private void OnDisable()
    {
        Conditions.OnCastleDamaged -= CastleDamagedAnimation;
    }

    private void CastleDamagedAnimation()
    {
        if (!isDestroyed)
        {
            isDestroyed = true;
            explosionSpriteRenderer.gameObject.SetActive(true);
            this.ActionDelayedRealtime(0.2f, () => explosionSpriteRenderer.sprite = explosionSpriteSequence[0]);
            this.ActionDelayedRealtime(0.4f, () => explosionSpriteRenderer.sprite = explosionSpriteSequence[1]);
            this.ActionDelayedRealtime(0.6f, () => explosionSpriteRenderer.sprite = explosionSpriteSequence[2]);
            this.ActionDelayedRealtime(0.8f, () => explosionSpriteRenderer.sprite = explosionSpriteSequence[3]);
            this.ActionDelayedRealtime(1f, () => gameObject.SetActive(false));
        }
    }
}
