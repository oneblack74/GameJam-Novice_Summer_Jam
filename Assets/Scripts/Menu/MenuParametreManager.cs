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
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private TextMeshProUGUI sensibilityText;
    [SerializeField] private TextMeshProUGUI volumeText;
    private float sensibility = 1.0f;
    private float volume = 1.0f;
    private int currentResolutionIndex = 4;
    private bool isFullScreen = true;

    private void Start()
    {

        Init();
    }

    public void Init()
    {
        Debug.Log("Init");
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        foreach (DimScreen dimScreen in dimScreens)
        {
            options.Add(dimScreen.width + "x" + dimScreen.height);
        }
        resolutionDropdown.AddOptions(options);
        fullScreenToggle.isOn = Screen.fullScreen;
        UpdateSensibility();
        UpdateVolume();

        sensibility = SaveData.Instance.data.mouseSensitivity;
        sensibilitySlider.value = sensibility;
        sensibilityText.text = sensibility.ToString();

        currentResolutionIndex = SaveData.Instance.data.currentResolutionIndex;
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        isFullScreen = SaveData.Instance.data.isFullScreen;
        fullScreenToggle.isOn = isFullScreen;

        volume = SaveData.Instance.data.volume;
        volumeSlider.value = volume;
        volumeText.text = volume.ToString();

        Apply();
    }

    IEnumerator WaitForApply()
    {
        yield return new WaitForSeconds(0.5f);
        Apply();
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
        ApplySensibility();
        ApplyVolume();
        SaveData.Instance.Save();
        Debug.LogError("In Apply : data.notePadData.Count: " + SaveData.Instance.data.notePadData.Count);
    }

    public void ResetParam()
    {
        sensibility = 1f;
        isFullScreen = true;
        currentResolutionIndex = 4;
        volume = 1f;


        sensibilitySlider.value = sensibility;
        sensibilityText.text = sensibility.ToString();

        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        fullScreenToggle.isOn = isFullScreen;

        volumeSlider.value = volume;
        volumeText.text = volume.ToString();

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
        if (GameManager.Instance != null)
            GameManager.Instance.Player.MouseSensitivity = sensibility / 10;
    }

    public void UpdateVolume()
    {
        volume = (float)Math.Round((double)(volumeSlider.value), 2);
        volumeText.text = volume.ToString();
    }

    public void ApplyVolume()
    {
        SaveData.Instance.data.volume = volume;
        AudioListener.volume = volume;
    }
}

[System.Serializable]
public class DimScreen
{
    public int width;
    public int height;
}