using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleCanvasManager : MonoBehaviour
{
    public static ScaleCanvasManager Instance = null;

    public void UpdateAllCanvases()
    {
        Canvas[] canvas = FindObjectsOfType<Canvas>();
        for (int i = 0; i < canvas.Length; i++)
        {

        }
        Canvas.ForceUpdateCanvases();
    }
}
