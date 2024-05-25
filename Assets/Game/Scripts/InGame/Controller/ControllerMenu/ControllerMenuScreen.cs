using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ControllerMenuScreen : MonoBehaviour
{
    [SerializeField] private Image blackoutBackground;

    [SerializeField] private RectTransform popupSettingsScreenRect;
    [SerializeField] private RectTransform encyclopediaScreenRect;
    [SerializeField] private RectTransform mainScreenRect;
    [SerializeField] private RectTransform defenseTowersScreenRect;
    [SerializeField] private RectTransform supportTowersScreenRect;
    [SerializeField] private RectTransform enemiesScreenRect;
    [SerializeField] private RectTransform resourcesScreenRect;
    [SerializeField] private RectTransform levelsScreenRect;

    private Vector2 popupSettingsStartAnchPos;
    private Vector2 encyclopediaAnchPos;
    private Vector2 mainScreenStartAnchPos;
    private Vector2 defenseTowersStartAnchPos;
    private Vector2 supportTowersStartAnchPos;
    private Vector2 enemiesStartAnchPos;
    private Vector2 resourcesStartAnchPos;
    private Vector2 levelsStartAnchPos;

    private int activePopupsCounter;

    [SerializeField] private ViewMenuParallaxBackground parallaxBackground;
    private ViewMenuDefenseTowers viewMenuDefenseTowers;
    private ViewMenuSupportTowers viewMenuSupportTowers;
    private ViewMenuEnemies viewMenuEnemies;
    private ViewMenuResources viewMenuResources;

    public void Init(ViewMenuDefenseTowers viewMenuDefenseTowers,
        ViewMenuSupportTowers viewMenuSupportTowers,
        ViewMenuEnemies viewMenuEnemies,
        ViewMenuResources viewMenuResources)
    {
        //cache references
        this.viewMenuDefenseTowers = viewMenuDefenseTowers;
        this.viewMenuSupportTowers = viewMenuSupportTowers;
        this.viewMenuEnemies = viewMenuEnemies;
        this.viewMenuResources = viewMenuResources;

        // null X pos for all additional screens on top
        defenseTowersScreenRect.anchoredPosition = new Vector2(0f, defenseTowersScreenRect.anchoredPosition.y);
        supportTowersScreenRect.anchoredPosition = new Vector2(0f, supportTowersScreenRect.anchoredPosition.y);
        enemiesScreenRect.anchoredPosition = new Vector2(0f, enemiesScreenRect.anchoredPosition.y);
        resourcesScreenRect.anchoredPosition = new Vector2(0f, resourcesScreenRect.anchoredPosition.y);
        levelsScreenRect.anchoredPosition = new Vector2(0f, levelsScreenRect.anchoredPosition.y);

        popupSettingsStartAnchPos = popupSettingsScreenRect.anchoredPosition;
        encyclopediaAnchPos = encyclopediaScreenRect.anchoredPosition;
        mainScreenStartAnchPos = mainScreenRect.anchoredPosition;
        defenseTowersStartAnchPos = defenseTowersScreenRect.anchoredPosition;
        supportTowersStartAnchPos = supportTowersScreenRect.anchoredPosition;
        enemiesStartAnchPos = enemiesScreenRect.anchoredPosition;
        resourcesStartAnchPos = resourcesScreenRect.anchoredPosition;
        levelsStartAnchPos = levelsScreenRect.anchoredPosition;

        levelsScreenRect.gameObject.SetActive(false);

        blackoutBackground.color = new Color(blackoutBackground.color.r, blackoutBackground.color.g, blackoutBackground.color.b, 0f);
        blackoutBackground.gameObject.SetActive(false);
    }

    private void CheckBlackoutStatus() 
    {
        if (activePopupsCounter == 0)
        {
            blackoutBackground.DOFade(0f, 0.5f).
                OnComplete(() => blackoutBackground.gameObject.SetActive(false));
        }
        else
        {
            blackoutBackground.gameObject.SetActive(true);
            blackoutBackground.DOFade(0.5f, 0.5f);
        }
    }
        

    internal void OpenPopupSettings()
    {
        activePopupsCounter++;
        CheckBlackoutStatus();
        popupSettingsScreenRect.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.InOutSine);
    }

    internal void ClosePopupSettings()
    {
        activePopupsCounter--;
        CheckBlackoutStatus();
        popupSettingsScreenRect.DOAnchorPos(popupSettingsStartAnchPos, 0.5f).SetEase(Ease.InOutSine);
    }

    internal void OpenEncyclopedia()
    {
        mainScreenRect.DOAnchorPos(new Vector2(0f, -Screen.height), 1f).SetEase(Ease.InOutSine);
        defenseTowersScreenRect.DOAnchorPos(defenseTowersStartAnchPos, 1f).SetEase(Ease.InOutSine).OnComplete(() => viewMenuDefenseTowers.WindowHide());
        supportTowersScreenRect.DOAnchorPos(supportTowersStartAnchPos, 1f).SetEase(Ease.InOutSine).OnComplete(() => viewMenuSupportTowers.WindowHide());
        enemiesScreenRect.DOAnchorPos(enemiesStartAnchPos, 1f).SetEase(Ease.InOutSine).OnComplete(() => viewMenuSupportTowers.WindowHide());
        resourcesScreenRect.DOAnchorPos(resourcesStartAnchPos, 1f).SetEase(Ease.InOutSine).OnComplete(() => viewMenuSupportTowers.WindowHide());
        encyclopediaScreenRect.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutSine);
        levelsScreenRect.DOAnchorPos(levelsStartAnchPos, 1f).SetEase(Ease.InOutSine);
        parallaxBackground.SetParallaxBgLevel(ViewMenuParallaxBackground.ParallaxDepth.middle);
    }

    internal void CloseEncyclopedia()
    {
        mainScreenRect.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutSine);
        encyclopediaScreenRect.DOAnchorPos(encyclopediaAnchPos, 1f).SetEase(Ease.InOutSine);
        parallaxBackground.SetParallaxBgLevel(ViewMenuParallaxBackground.ParallaxDepth.bottom);
    }

    internal void OpenDefenseTowersScreen()
    {
        viewMenuDefenseTowers.WindowShow();
        defenseTowersScreenRect.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutSine);
        encyclopediaScreenRect.DOAnchorPos(new Vector2(0f, -Screen.height), 1f).SetEase(Ease.InOutSine);
        parallaxBackground.SetParallaxBgLevel(ViewMenuParallaxBackground.ParallaxDepth.top);
    }

    internal void OpenSupportTowersScreen()
    {
        viewMenuSupportTowers.WindowShow();
        supportTowersScreenRect.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutSine);
        encyclopediaScreenRect.DOAnchorPos(new Vector2(0f, -Screen.height), 1f).SetEase(Ease.InOutSine);
        parallaxBackground.SetParallaxBgLevel(ViewMenuParallaxBackground.ParallaxDepth.top);
    }

    internal void OpenEnemiesScreen()
    {
        viewMenuEnemies.WindowShow();
        enemiesScreenRect.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutSine);
        encyclopediaScreenRect.DOAnchorPos(new Vector2(0f, -Screen.height), 1f).SetEase(Ease.InOutSine);
        parallaxBackground.SetParallaxBgLevel(ViewMenuParallaxBackground.ParallaxDepth.top);
    }

    internal void OpenResourcesScreen()
    {
        viewMenuResources.WindowShow();
        resourcesScreenRect.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutSine);
        encyclopediaScreenRect.DOAnchorPos(new Vector2(0f, -Screen.height), 1f).SetEase(Ease.InOutSine);
        parallaxBackground.SetParallaxBgLevel(ViewMenuParallaxBackground.ParallaxDepth.top);
    }

    internal void OpenLevels()
    {
        levelsScreenRect.gameObject.SetActive(true);
        mainScreenRect.DOAnchorPos(new Vector2(0f, -Screen.height), 1f).SetEase(Ease.InOutSine);
        levelsScreenRect.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutSine);
        parallaxBackground.SetParallaxBgLevel(ViewMenuParallaxBackground.ParallaxDepth.top);
    }

    internal void CloseLevels()
    {
        mainScreenRect.DOAnchorPos(Vector2.zero, 1f).SetEase(Ease.InOutSine);
        levelsScreenRect.DOAnchorPos(levelsStartAnchPos, 1f).SetEase(Ease.InOutSine).OnComplete(() => levelsScreenRect.gameObject.SetActive(false));
        parallaxBackground.SetParallaxBgLevel(ViewMenuParallaxBackground.ParallaxDepth.bottom);
    }
}