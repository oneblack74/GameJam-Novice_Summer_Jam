using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public PlayerInput inputs;
    public AudioSource audioSource;
    private string fileSaveName;

    private readonly Dictionary<int, ItemDefinition> itemDico = new();

    void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        ItemDefinition[] items = Resources.LoadAll<ItemDefinition>("Items");

        foreach (ItemDefinition item in items)
        {
            itemDico.Add(item.GetID, item);
        }
    }

    public ItemDefinition ConvertIdToItem(int itemID)
    {
        return itemDico[itemID];
    }

    public void PlayAudioClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopAudioClip()
    {
        audioSource.Stop();
    }

    void Start()
    {
        Debug.Log("Gamemanger Loaded");
    }


    public PlayerInput GetInputs
    {
        get { return inputs; }
    }

    public string FileSaveName
    {
        get { return fileSaveName; }
    }

    public void LockCursor(bool state)
    {
        if (state == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
