using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfosManager : MonoBehaviour
{
    public static InfosManager Instance = null;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoText;

    [SerializeField] private Infos[] infos;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OpenInfoPanel(string key)
    {
        foreach (Infos info in infos)
        {
            if (info.key == key)
            {
                infoPanel.SetActive(true);
                infoText.text = info.info;
                StartCoroutine(CloseInfoPanelCoroutine(5f));
            }
        }
    }

    public IEnumerator CloseInfoPanelCoroutine(float time)
    {
        yield return new WaitForSeconds(time);

        CloseInfoPanel();
    }

    public void CloseInfoPanel()
    {
        infoPanel.SetActive(false);
    }

}

[System.Serializable]
public class Infos
{
    public string key;
    public string info;
}