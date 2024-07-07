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
        CheckFileExist();
    }

    void Start()
    {
        //StartCoroutine(LoadDataWithDelay());
        Load();
        Debug.Log("In Start: data.notePadData.Count: " + data.notePadData.Count);
    }

    public void Save()
    {
        FileManager.SaveToFile(data, "notePadData.json");
    }

    public IEnumerator LoadDataWithDelay()
    {
        yield return new WaitForSeconds(1f);
        Load();
    }

    public void CheckFileExist()
    {
        if (FileManager.FileExist("notePadData.json"))
        {
            fileIsExist = true;
        }
        else
        {
            fileIsExist = false;
        }
        Debug.Log("fileIsExist: " + fileIsExist);
    }

    public void Load()
    {
        if (!fileIsExist)
            ResetDataWithoutParam();
        else
            data = FileManager.LoadFromFile<Data>("notePadData.json");
        Debug.Log("In Load : data.notePadData.Count: " + data.notePadData.Count);
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
        float tmpVolume = 1.0f;

        if (fileIsExist)
        {
            tmpCurrentResolutionIndex = data.currentResolutionIndex;
            tmpIsFullScreen = data.isFullScreen;
            tmpMouseSensitivity = data.mouseSensitivity;
            tmpVolume = data.volume;

        }

        data = new Data();
        data.notePadData = new List<string>();
        data.notePadData.Add("");
        data.notePadData.Add("");
        data.notePadData.Add("");
        data.currentResolutionIndex = tmpCurrentResolutionIndex;
        data.isFullScreen = tmpIsFullScreen;
        data.mouseSensitivity = tmpMouseSensitivity;
        data.volume = tmpVolume;
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
    public float volume = 1.0f;
    public int deathCounter = 0;
}
