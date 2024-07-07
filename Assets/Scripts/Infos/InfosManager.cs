using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfosManager : MonoBehaviour
{
    public static InfosManager Instance = null;

    [SerializeField] private GameObject infoPanel;
    [SerializeField] private TextMeshProUGUI infoText;
    [SerializeField] private float timeBeforeClose = 10f;
    [SerializeField] private Infos[] infos;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void OpenInfoPanel(string key)
    {
        foreach (Infos info in infos)
        {
            if (info.key == key)
            {
                infoPanel.SetActive(true);
                infoText.text = info.info;
                StartCoroutine(CloseInfoPanelCoroutine(timeBeforeClose));
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