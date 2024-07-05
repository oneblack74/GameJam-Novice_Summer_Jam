using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public PlayerInput inputs;

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

    void Start()
    {
        Debug.Log("Gamemanger Loaded");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
