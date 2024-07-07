using System.Collections;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathManager : MonoBehaviour
{
    [SerializeField] private NotePadManager notePadManager;
    [SerializeField] private int killAfterSeconds = 60;
    [SerializeField] private int reloadAfterSeconds = 5;
    [SerializeField] private Counter[] counters;
    private Coroutine coroutine;
    void Start()
    {
        coroutine = StartCoroutine(KillPlayer());
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
        notePadManager.LoadNotePadData();// TODO : A virer
        notePadManager.SaveNotePadData();
        Death();
    }

    IEnumerator ReloadGame()
    {
        yield return new WaitForSeconds(reloadAfterSeconds);
        SaveData.Instance.Save();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void StopCounter()
    {
        foreach (Counter counter in counters)
        {
            counter.Stop();
        }
        GameManager.Instance.StopAudioClip();
        StopCoroutine(coroutine);
    }
}