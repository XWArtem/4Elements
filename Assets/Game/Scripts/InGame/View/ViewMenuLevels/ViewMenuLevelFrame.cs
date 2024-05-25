using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ViewMenuLevelFrame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelIndex;
    [SerializeField] private Sprite lvlLocked, lvlOpened;
    [SerializeField] private Image image;

    private int correspondingLvlIndex;

    private Button button;

    public void InitFrame(int lvlIndex, bool isOpened, int stars = 3)
    {
        button = GetComponent<Button>();
        correspondingLvlIndex = lvlIndex;

        UpdateFrame(lvlIndex, isOpened, stars);


        button.onClick.AddListener(ButtonAnimationActionRoutine);
    }

    public void UpdateFrame(int lvlIndex, bool isOpened, int stars = 3)
    {
        image.sprite = isOpened ? lvlOpened : lvlLocked;
        if (isOpened)
        {
            image.fillAmount = 0.333f * (float)stars;
        }
        else image.fillAmount = 1f;

        image.SetNativeSize();
        image.gameObject.SetActive(lvlIndex != UserSettingsStorage.Instance.MaxLevelIndexOpened);
        levelIndex.text = $"{correspondingLvlIndex}";
        button.interactable = isOpened;
    }

    private void OnDestroy()
    {
        button.onClick.RemoveAllListeners();
    }

    private void ButtonAnimationActionRoutine()
    {
        if (ViewMenuLevels.LevelSelectionLocked) return;

        ViewMenuLevels.LevelSelectionLocked = true;
        AudioManager.Instance.PlayTapSound();
        button.GetComponent<RectTransform>().DOScale(0.8f, 0.5f).
            SetEase(Ease.OutElastic).
            OnComplete(GoToLevel);
    }

    private void GoToLevel()
    {
        UserSettingsStorage.Instance.CurrentLevelIndex = correspondingLvlIndex;
        SceneManager.LoadScene(SettingsMenu.TDSceneName);
    }
}
