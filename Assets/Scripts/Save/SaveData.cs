using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance = null;

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

    public void Save()
    {
        FileManager.SaveToFile(data, "notePadData.json");
    }

    public void Load()
    {
        data = FileManager.LoadFromFile<Data>("notePadData.json");
    }
}

[System.Serializable]
public class Data
{
    public List<string> notePadData;
}
