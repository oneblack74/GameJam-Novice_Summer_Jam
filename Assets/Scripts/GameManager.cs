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

    void Awake()
    {
        if (GetInstance() != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public static GameManager GetInstance()
    {
        return Instance;
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
