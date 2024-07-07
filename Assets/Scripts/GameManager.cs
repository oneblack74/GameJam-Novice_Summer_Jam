using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    public PlayerInput inputs;
    public AudioSource audioSource;
    public AudioClip backgroundMusic;
    private string fileSaveName;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private DeathManager deathManager;

    private readonly Dictionary<int, ItemDefinition> itemDico = new();

    void Awake()
    {
        if (GameManager.Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

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
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }


    public PlayerInput GetInputs
    {
        get { return inputs; }
    }

    public string FileSaveName
    {
        get { return fileSaveName; }
    }

    public PlayerController Player
    {
        get { return playerController; }
    }

    public DeathManager GetDeathManager
    {
        get { return deathManager; }
    }

    public void LockCursor(bool state)
    {
        if (state == true)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
