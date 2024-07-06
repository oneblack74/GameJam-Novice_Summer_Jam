using System.IO;
using UnityEngine;

public static class FileManager
{
    public static void SaveToFile<T>(T data, string filename)
    {
        // sérialisation du fichier
        string json = JsonUtility.ToJson(data);

        // chemin du fichier
        string path = Path.Combine(Application.persistentDataPath, filename);
        Debug.Log("Save as: " + path);

        // écriture du fichier
        File.WriteAllText(path, json);
    }

    public static T LoadFromFile<T>(string filename)
    {
        // chemin du fichier
        string path = Path.Combine(Application.persistentDataPath, filename);

        // si le fichier n'existe pas, on retourne la valeur par défaut
        if (!File.Exists(path))
        {
            return default(T);
        }

        // lecture du fichier
        string json = File.ReadAllText(path);

        Debug.Log("Load file");
        // désérialisation du fichier
        return JsonUtility.FromJson<T>(json);
    }

    public static void DeleteFile(string filename)
    {
        // chemin du fichier
        string path = Path.Combine(Application.persistentDataPath, filename);

        // suppression du fichier
        File.Delete(path);
    }

    public static bool FileExist(string filename)
    {
        // chemin du fichier
        string path = Path.Combine(Application.persistentDataPath, filename);

        // vérification de l'existence du fichier
        return File.Exists(path);
    }
}
