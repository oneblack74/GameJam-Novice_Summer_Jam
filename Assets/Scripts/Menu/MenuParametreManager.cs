using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuParametreManager : MonoBehaviour
{
    [SerializeField] private DimScreen[] dimScreens;
    [SerializeField] private TMP_Dropdown resolutionDropdown;
    [SerializeField] private Toggle fullScreenToggle;
    private int currentResolutionIndex = 0;
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
    }

    public void SelectResolution()
    {
        currentResolutionIndex = resolutionDropdown.value;
    }

    public void ApplyResolution()
    {
        Screen.SetResolution(dimScreens[currentResolutionIndex].width, dimScreens[currentResolutionIndex].height, true);
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
    }
}

[System.Serializable]
public class DimScreen
{
    public int width;
    public int height;
}