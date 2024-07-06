using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private NotePadManager notePadManager;
    [SerializeField] private int killAfterSeconds = 60;
    [SerializeField] private int reloadAfterSeconds = 5;
    void Start()
    {
        notePadManager.LoadNotePadData();
        StartCoroutine(KillPlayer());
    }

    [ContextMenu("Kill Player")]
    public void Death()
    {
        StartCoroutine(GameManager.Instance.Player.Die());
        StartCoroutine(ReloadGame());
    }

    IEnumerator KillPlayer()
    {
        yield return new WaitForSeconds(killAfterSeconds);
        notePadManager.CloseNotePad();
        notePadManager.SaveNotePadData();
        Death();
    }

    IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(reloadAfterSeconds);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}