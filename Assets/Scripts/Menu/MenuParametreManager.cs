using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class MenuParametreManager : MonoBehaviour
{
    [SerializeField] private DimScreen[] dimScreens;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Slider sensibilitySlider;
    [SerializeField] private TextMeshProUGUI sensibilityText;
    private float sensibility = 1.0f;
    private int currentResolutionIndex = 4;
    private bool isFullScreen = true;

    private void Start()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (DimScreen dimScreen in dimScreens)
        {
            options.Add(dimScreen.width + "x" + dimScreen.height);
        }
        resolutionDropdown.AddOptions(options);
        fullScreenToggle.isOn = Screen.fullScreen;
        UpdateSensibility();

        sensibility = SaveData.Instance.data.mouseSensitivity;
        sensibilitySlider.value = sensibility;
        sensibilityText.text = sensibility.ToString();

        currentResolutionIndex = SaveData.Instance.data.currentResolutionIndex;
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        isFullScreen = SaveData.Instance.data.isFullScreen;
        fullScreenToggle.isOn = isFullScreen;

    }

    public void SelectResolution()
    {
        currentResolutionIndex = resolutionDropdown.value;
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(dimScreens[currentResolutionIndex].width, dimScreens[currentResolutionIndex].height, isFullScreen);
    }


    public void FullScreen()
    {
        isFullScreen = fullScreenToggle.isOn;
        Screen.fullScreen = isFullScreen;
    }

    public void Apply()
    {
        ApplyResolution();
        FullScreen();
        SaveData.Instance.Save();
    }

    public void ResetParam()
    {
        sensibility = SaveData.Instance.data.mouseSensitivity;
        sensibilitySlider.value = sensibility;
        sensibilityText.text = sensibility.ToString();

        currentResolutionIndex = SaveData.Instance.data.currentResolutionIndex;
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        isFullScreen = SaveData.Instance.data.isFullScreen;
        fullScreenToggle.isOn = isFullScreen;
        Apply();
    }

    public void UpdateSensibility()
    {
        sensibility = (float)Math.Round((double)(sensibilitySlider.value), 2);
        sensibilityText.text = sensibility.ToString();
    }

    public void ApplySensibility()
    {
        SaveData.Instance.data.mouseSensitivity = sensibility;
        SaveData.Instance.data.currentResolutionIndex = currentResolutionIndex;
        SaveData.Instance.data.isFullScreen = isFullScreen;
        SaveData.Instance.Save();
        // todo Apply sensibility
    }
}

[System.Serializable]
public class DimScreen
{
    public int width;
    public int height;
}