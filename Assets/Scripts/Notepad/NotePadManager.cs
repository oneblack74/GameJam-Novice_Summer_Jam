using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NotePadManager : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private Button previousPage;
    [SerializeField] private Button nextPage;
    [SerializeField] private GameObject notePad;
    private bool isNotePadOpen = false;
    private bool haveNotePad = false;

    private List<string> notePadData;
    public List<string> NotePadData
    {
        get { return notePadData; }
        set { notePadData = value; }
    }
    private int numPage = 0;
    [SerializeField] private int nbPage = 3;

    void Awake()
    {
        gameManager = GameManager.Instance;
    }

    void Start()
    {
        gameManager.inputs.actions["OpenNotepad"].performed += OpenNotePad;
        notePadData = new List<string>();
        for (int i = 0; i < nbPage; i++)
        {
            notePadData.Add("");
        }
        inputField.text = notePadData[0];
    }

    public void HandlerTextChange()
    {
        Debug.Log("Text changed");
    }

    private void OpenNotePad(InputAction.CallbackContext context)
    {
        if (haveNotePad)
        {
            isNotePadOpen = !isNotePadOpen;
            notePad.SetActive(isNotePadOpen);
            if (isNotePadOpen)
            {
                inputField.text = notePadData[numPage];
                if (numPage == 0)
                {
                    previousPage.gameObject.SetActive(false);
                    nextPage.gameObject.SetActive(true);
                }
                else if (numPage == nbPage - 1)
                {
                    previousPage.gameObject.SetActive(true);
                    nextPage.gameObject.SetActive(false);
                }
                else
                {
                    previousPage.gameObject.SetActive(true);
                    nextPage.gameObject.SetActive(true);
                }
            }
        }
    }

    public void CloseNotePad()
    {
        isNotePadOpen = false;
        notePad.SetActive(isNotePadOpen);
        notePadData[numPage] = inputField.text;
    }

    public void NextPage()
    {
        notePadData[numPage] = inputField.text;
        if (numPage != nbPage - 1)
        {
            numPage++;
            if (numPage == nbPage - 1)
                nextPage.gameObject.SetActive(false);
        }


        previousPage.gameObject.SetActive(true);
        inputField.text = notePadData[numPage];
    }

    [ContextMenu("ActiveNotePad")]
    public void ActiveNotePad()
    {
        haveNotePad = true;
        notePadData = SaveData.Instance.data.notePadData;
        InfosManager.Instance.OpenInfoPanel("GetNotePad");
    }

    public void PreviousPage()
    {
        notePadData[numPage] = inputField.text;
        if (numPage != 0)
        {
            numPage--;
            if (numPage == 0)
                previousPage.gameObject.SetActive(false);
        }


        nextPage.gameObject.SetActive(true);
        inputField.text = notePadData[numPage];
    }

    [ContextMenu("SaveNotePadData")]
    public void SaveNotePadData()
    {
        notePadData[numPage] = inputField.text;
        SaveData.Instance.data.notePadData = notePadData;
        SaveData.Instance.Save();
    }

    [ContextMenu("LoadNotePadData")]
    public void LoadNotePadData()
    {
        SaveData.Instance.Load();
        notePadData = SaveData.Instance.data.notePadData;
        inputField.text = notePadData[numPage];
    }
}
