using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private string nameSceneToLoad;
    [SerializeField] private Button BtnLoadGame;

    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(nameSceneToLoad);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void Start()
    {
        string path = Path.Combine(Application.persistentDataPath, "notePadData.json");
        if (!File.Exists(path))
            BtnLoadGame.interactable = false;
    }

    public void LoadGame()
    {
        SaveData.Instance.Load();
        LoadScene();
    }

    public void NewGame()
    {
        SaveData.Instance.Save();
        SaveData.Instance.ResetDataWithoutParam();
        LoadScene();
    }

    [ContextMenu("SupprimerFichier")]
    public void SupprimerFichier()
    {
        SaveData.Instance.SupprimerFichier();
        BtnLoadGame.interactable = false;
    }

}
