using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuParametreManager : MonoBehaviour
{
    [SerializeField] private DimScreen[] dimScreens;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

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

}

[System.Serializable]
public class DimScreen
{
    public int width;
    public int height;
}