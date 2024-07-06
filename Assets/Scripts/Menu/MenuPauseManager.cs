using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuPauseManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject crossHair;
    private bool isMenuPauseOpen = false;

    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.inputs.actions["MenuPause"].performed += OpenMenuPause;
    }

    private void OpenMenuPause(InputAction.CallbackContext context)
    {
        isMenuPauseOpen = !isMenuPauseOpen;
        menuPause.SetActive(isMenuPauseOpen);
        gameManager.Player.BlockPlayerToggle();
        if (isMenuPauseOpen)
        {
            gameManager.LockCursor(false);
            crossHair.SetActive(false);
        }
        else
        {
            gameManager.LockCursor(true);
            crossHair.SetActive(true);
        }
    }

    public void GoToMainMenu()
    {
        SaveData.Instance.Save();
        SceneManager.LoadScene("MainMenu");
    }
}
