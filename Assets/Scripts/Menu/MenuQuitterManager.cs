using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuQuitterManager : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SaveData.Instance.Save();
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        SaveData.Instance.Save();
        Application.Quit();
    }
}
