using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance = null;

    public bool fileIsExist = false;

    public Data data;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    void Start()
    {
        Load();
    }

    public void Save()
    {
        FileManager.SaveToFile(data, "notePadData.json");
    }

    public void Load()
    {
        data = FileManager.LoadFromFile<Data>("notePadData.json");
        if (data == null)
        {
            ResetDataWithoutParam();
            fileIsExist = false;
        }
        else
        {
            fileIsExist = true;
        }
    }

    [ContextMenu("SupprimerFichier")]
    public void SupprimerFichier()
    {
        FileManager.DeleteFile("notePadData.json");
    }

    public void ResetDataWithoutParam()
    {
        int tmpCurrentResolutionIndex = 4;
        bool tmpIsFullScreen = true;
        float tmpMouseSensitivity = 0.1f;

        if (fileIsExist)
        {
            tmpCurrentResolutionIndex = data.currentResolutionIndex;
            tmpIsFullScreen = data.isFullScreen;
            tmpMouseSensitivity = data.mouseSensitivity;

        }

        data = new Data();
        data.notePadData = new List<string>();
        data.notePadData.Add("");
        data.notePadData.Add("");
        data.notePadData.Add("");
        data.currentResolutionIndex = tmpCurrentResolutionIndex;
        data.isFullScreen = tmpIsFullScreen;
        data.mouseSensitivity = tmpMouseSensitivity;
        Save();
    }
}

[System.Serializable]
public class Data
{
    public List<string> notePadData;
    public float mouseSensitivity = 0.1f;
    public int currentResolutionIndex = 4;
    public bool isFullScreen = true;
    public int deathCounter = 0;
}
