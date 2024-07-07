using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MenuPauseManager : MonoBehaviour
{
    private GameManager gameManager;
    [SerializeField] private GameObject menuPause;
    [SerializeField] private GameObject crossHair;
    private bool isMenuPauseOpen = false;

    void Awake()
    {
        isMenuPauseOpen = false;
    }

    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.inputs.actions["MenuPause"].performed += OpenMenuPause;
        menuPause.SetActive(isMenuPauseOpen);
    }

    private void OpenMenuPause(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Player.IsBlocked && !isMenuPauseOpen)
        {
            return;
        }
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

    public void QuitGame()
    {
        SaveData.Instance.Save();
        Application.Quit();
    }
}
