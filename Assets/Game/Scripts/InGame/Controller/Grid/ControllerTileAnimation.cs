using DG.Tweening;
using UnityEngine;

public class ControllerTileAnimation : MonoBehaviour
{
    [SerializeField] private Transform pointer;
    [SerializeField] private Transform shovelLeftTop;
    [SerializeField] private Transform shovelLeftBottom;
    [SerializeField] private Transform shovelRight;

    [SerializeField] private SpriteRenderer iron;
    [SerializeField] private SpriteRenderer diamond;

    private const float AnimationLength = 0.5f;

    public void Init()
    {
        pointer.gameObject.SetActive(false);
        iron.gameObject.SetActive(false);
        diamond.gameObject.SetActive(false);
        pointer.GetComponent<SpriteRenderer>().color = Color.white;

        shovelLeftTop.gameObject.SetActive(false);
        shovelLeftBottom.gameObject.SetActive(false);
        shovelRight.gameObject.SetActive(false);
    }

    public void PointerClickAnimation()
    {
        pointer.gameObject.SetActive(true);
        pointer.DOScale(new Vector3(0.5f, 0.5f), AnimationLength).SetUpdate(true);
        pointer.GetComponent<SpriteRenderer>().DOFade(0f, AnimationLength).SetUpdate(true);

        this.ActionDelayed(0.6f, () => pointer.transform.localScale = Vector2.one);
        this.ActionDelayed(0.6f, () => pointer.gameObject.SetActive(false));
        this.ActionDelayed(0.61f, () => pointer.GetComponent<SpriteRenderer>().color = Color.white);
    }

    public void DigAnimation()
    {
        shovelLeftBottom.gameObject.SetActive(true);
        this.ActionDelayed(0.3f, () => shovelLeftTop.gameObject.SetActive(true));
        this.ActionDelayed(0.15f, () => shovelRight.gameObject.SetActive(true));

        shovelLeftBottom.DOMove(new Vector2(shovelLeftBottom.transform.position.x - 0.2f, shovelLeftBottom.transform.position.y - 0.2f), AnimationLength).
            SetEase(Ease.OutElastic, 0.5f).
            SetUpdate(true).
            OnComplete(() => shovelLeftBottom.gameObject.SetActive(false));

        this.ActionDelayed(0.3f, () =>
        shovelLeftTop.DOMove(new Vector2(shovelLeftTop.transform.position.x - 0.2f, shovelLeftTop.transform.position.y - 0.2f), AnimationLength).
            SetEase(Ease.OutElastic, 0.5f).
            SetUpdate(true).
            OnComplete(() => shovelLeftTop.gameObject.SetActive(false)));

        this.ActionDelayed(0.15f, () =>
        shovelRight.DOMove(new Vector2(shovelRight.transform.position.x + 0.2f, shovelRight.transform.position.y - 0.2f), AnimationLength).
            SetEase(Ease.OutElastic, 0.5f).
            SetUpdate(true).
            OnComplete(() => shovelRight.gameObject.SetActive(false)));
    }

    public void GetIronAnimation()
    {
        iron.color = Color.white;
        iron.gameObject.SetActive(true);
        iron.transform.DOLocalMoveY(2f, 2f).
            OnComplete(() => iron.transform.localPosition = Vector2.zero).
            SetUpdate(true);

        iron.DOFade(0f, 2f).
            OnComplete(() => iron.gameObject.SetActive(false)).
            SetUpdate(true);
    }

    public void GetDiamondAnimation()
    {
        diamond.color = Color.white;
        diamond.gameObject.SetActive(true);
        diamond.transform.DOLocalMoveY(2f, 2f).
            OnComplete(() => diamond.transform.localPosition = Vector2.zero).
            SetUpdate(true);

        diamond.DOFade(0f, 2f).
            OnComplete(() => diamond.gameObject.SetActive(false)).
            SetUpdate(true);
    }
}