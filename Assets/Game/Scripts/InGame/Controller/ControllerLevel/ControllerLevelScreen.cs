using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class ControllerLevelScreen : MonoBehaviour
{
    [SerializeField] private Button buttonPause;
    [SerializeField] private Button buttonResume;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonGoToMenu;

    // popup win
    [SerializeField] private Button popupWinNextLevel;
    [SerializeField] private Button popupWinGoToMenu;
    [SerializeField] private TextMeshProUGUI popupWinDescription;
    [SerializeField] private List<Image> popupWinStars;
    [SerializeField] private Sprite starFailed, starCompleted;
    // popup lose
    [SerializeField] private Button popupLoseRestart;
    [SerializeField] private Button popupLoseGoToMenu;


    [SerializeField] private Image blackoutBackground;
    [SerializeField] private RectTransform popupPauseScreenRect;
    [SerializeField] private RectTransform popupWinScreenRect;
    [SerializeField] private RectTransform popupLoseScreenRect;

    private int activePopupsCounter;

    private Vector2 popupSettingsStartAnchPos;
    private Vector2 popupWinStartAnchPos;
    private Vector2 popupLoseStartAnchPos;

    public void Init()
    {
        popupSettingsStartAnchPos = popupPauseScreenRect.anchoredPosition;
        popupWinStartAnchPos = popupWinScreenRect.anchoredPosition;
        popupLoseStartAnchPos = popupLoseScreenRect.anchoredPosition;

        blackoutBackground.color = new Color(blackoutBackground.color.r, blackoutBackground.color.g, blackoutBackground.color.b, 0f);
        blackoutBackground.gameObject.SetActive(false);

        buttonPause.onClick.AddListener(Pause);
        buttonResume.onClick.AddListener(UnPause);
        buttonRestart.onClick.AddListener(Restart);
        buttonGoToMenu.onClick.AddListener(GoToMenu);

        popupWinNextLevel.onClick.AddListener(Restart);
        popupWinGoToMenu.onClick.AddListener(GoToMenu);

        popupLoseRestart.onClick.AddListener(Restart);
        popupLoseGoToMenu.onClick.AddListener(GoToMenu);

        Conditions.OnGameWin += GamePreWin;
        Conditions.OnGameOver += GameLost;
    }

    private void OnDestroy()
    {
        buttonPause.onClick.RemoveAllListeners();
        buttonResume.onClick.RemoveAllListeners();
        buttonRestart.onClick.RemoveAllListeners();
        buttonGoToMenu.onClick.RemoveAllListeners();

        popupWinNextLevel.onClick.RemoveAllListeners();
        popupWinGoToMenu.onClick.RemoveAllListeners();

        popupLoseRestart.onClick.RemoveAllListeners();
        popupLoseGoToMenu.onClick.RemoveAllListeners();

        Conditions.OnGameWin -= GamePreWin;
        Conditions.OnGameOver -= GameLost;
    }

    private void CheckBlackoutStatus()
    {
        if (activePopupsCounter == 0)
        {
            blackoutBackground.DOFade(0f, 0.5f).
                SetUpdate(true).
                OnComplete(() => blackoutBackground.gameObject.SetActive(false));
        }
        else
        {
            blackoutBackground.gameObject.SetActive(true);
            blackoutBackground.DOFade(0.5f, 0.5f).SetUpdate(true);
        }
    }

    internal void Pause()
    {
        AudioManager.Instance.PlayTapSound();
        SimpleDILevel.Instance.TimeScaleHandler.SetTimeScale(0f);
        activePopupsCounter++;
        CheckBlackoutStatus();
        popupPauseScreenRect.DOAnchorPos(Vector2.zero, 0.5f).SetEase(Ease.InOutSine).SetUpdate(true);
    }

    internal void UnPause()
    {
        AudioManager.Instance.PlayTapSound();
        SimpleDILevel.Instance.TimeScaleHandler.SetTimeScale(1f);
        activePopupsCounter--;
        CheckBlackoutStatus();
        popupPauseScreenRect.DOAnchorPos(popupSettingsStartAnchPos, 0.5f).SetEase(Ease.InOutSine).SetUpdate(true);
    }

    private void Restart()
    {
        AudioManager.Instance.PlayTapSound();
        SimpleDILevel.Instance.TimeScaleHandler.SetTimeScale(1f);
        StopAllCoroutines();
        SceneManager.LoadScene(SettingsLevel.TDSceneName);
    }

    private void GoToMenu()
    {
        AudioManager.Instance.PlayTapSound();
        SimpleDILevel.Instance.TimeScaleHandler.SetTimeScale(1f);
        SceneManager.LoadScene(SettingsLevel.MenuSceneName);
    }

    private void GameLost()
    {
        AudioManager.Instance.PlayClipByName("13_lost_game");
        activePopupsCounter++;
        CheckBlackoutStatus();
        SimpleDILevel.Instance.TimeScaleHandler.SetTimeScale(0f);
        popupLoseScreenRect.DOAnchorPos(Vector2.zero, 0.5f).
            SetEase(Ease.InOutSine).
            SetUpdate(true);
    }

    private void GamePreWin()
    {
        SimpleDILevel.Instance.TimeScaleHandler.SetTimeScale(1f);

        // fill win popup values
        popupWinDescription.text = $"Level {UserSettingsStorage.Instance.CurrentLevelIndex} Completed!";

        var stars = 0;
        if (DataStorageSingletone.Instance.CastleHP > 50) stars = 1;
        if (DataStorageSingletone.Instance.CastleHP > 150) stars = 2;
        if (DataStorageSingletone.Instance.CastleHP > 250) stars = 3;

        for (int i = 0; i < popupWinStars.Count; i++)
        {
            popupWinStars[i].sprite = (stars > i) ? starCompleted : starFailed;
            popupWinStars[i].transform.localScale = new Vector2(6f, 6f);
            popupWinStars[i].color = new Color(1f, 1f, 1f, 0f);
        }

        // write on data stars for this level
        string starsString = UserSettingsStorage.Instance.Stars;
        List<int> starsAll = starsString.Split(',').Select(Int32.Parse)?.ToList();
        if (starsAll[UserSettingsStorage.Instance.CurrentLevelIndex - 1] < stars)
        {
            starsAll[UserSettingsStorage.Instance.CurrentLevelIndex - 1] = stars;
        }
        UserSettingsStorage.Instance.Stars = string.Join(",", starsAll);

        popupWinNextLevel.gameObject.SetActive(UserSettingsStorage.Instance.CurrentLevelIndex != 30);

        AudioManager.Instance.PlayClipByName("14_win");

        UserSettingsStorage.Instance.CurrentLevelIndex++;

        this.ActionDelayedRealtime(3f, GameWin);
    }

    private void GameWin()
    {
        activePopupsCounter++;
        CheckBlackoutStatus();
        popupWinScreenRect.DOAnchorPos(Vector2.zero, 0.5f).
            SetEase(Ease.InOutSine).
            SetUpdate(true).
            OnComplete(() => ShowStarAnimation(0));
    }

    private void ShowStarAnimation(int starIndex)
    {
        if (starIndex > popupWinStars.Count - 1)
        {
            SimpleDILevel.Instance.TimeScaleHandler.SetTimeScale(0f);
            return;
        }
        popupWinStars[starIndex].DOFade(1f, 0.8f);
        popupWinStars[starIndex].transform.DOScale(Vector2.one, 0.8f).
                SetEase(Ease.InBack).
                OnComplete(() => ShowStarAnimation(++starIndex));
    }
}