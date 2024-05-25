using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ViewMenuSettings : MonoBehaviour
{
    [SerializeField] private GameObject commonSettingsFrame, resetProgressConfirmationFrame;
    [SerializeField] private TextMeshProUGUI frameTitle;

    private const string FrameTitleSettings = "Settings";
    private const string FrameTitleConfirmation = "Reset Progress";

    public void Init()
    {
        resetProgressConfirmationFrame.SetActive(false);
        commonSettingsFrame.SetActive(true);

        frameTitle.text = FrameTitleSettings;
    }

    public void OpenConfirmationFrame()
    {
        resetProgressConfirmationFrame.SetActive(true);
        commonSettingsFrame.SetActive(false);
        frameTitle.text = FrameTitleConfirmation;
    }

    public void CloseConfirmationFrame()
    {
        commonSettingsFrame.SetActive(true);
        resetProgressConfirmationFrame.SetActive(false);
        frameTitle.text = FrameTitleSettings;
    }
}
