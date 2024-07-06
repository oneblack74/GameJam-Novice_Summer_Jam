using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private NotePadManager notePadManager;

    void Start()
    {
        notePadManager.LoadNotePadData();
        StartCoroutine(KillPlayer());
    }

    public void Death()
    {
        StartCoroutine(GameManager.Instance.Player.Die());
        StartCoroutine(ReloadGame());
    }

    IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(60);
        notePadManager.CloseNotePad();
        notePadManager.SaveNotePadData();
        Death();
    }

    IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}