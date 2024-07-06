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
    private bool canActive = true;
    public bool CanActive
    {
        get { return canActive; }
        set { canActive = value; }
    }
    [SerializeField] private GameObject crossHair;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private List<string> notePadData;
    public List<string> NotePadData
    {
        get { return notePadData; }
        set { notePadData = value; }
    }
    private int numPage = 0;
    [SerializeField] private int nbPage = 3;


    void Start()
    {
        gameManager = GameManager.Instance;
        gameManager.inputs.actions["OpenNotepad"].performed += OpenNotePad;
        notePadData = new List<string>();
        for (int i = 0; i < nbPage; i++)
        {
            notePadData.Add("");
        }
        inputField.text = notePadData[0];
    }

    private void OpenNotePad(InputAction.CallbackContext context)
    {
        if (haveNotePad && canActive)
        {
            isNotePadOpen = !isNotePadOpen;
            if (crossHair != null)
                crossHair.SetActive(!isNotePadOpen);
            if (playerController != null)
                playerController.BlockPlayerToggle();
            notePad.SetActive(isNotePadOpen);
            if (isNotePadOpen)
            {
                gameManager.LockCursor(false);
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
            else
            {
                gameManager.LockCursor(true);
                notePadData[numPage] = inputField.text;
            }
        }
    }

    public void CloseNotePad()
    {
        isNotePadOpen = true;
        OpenNotePad(new InputAction.CallbackContext());
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
        if (SaveData.Instance.fileIsExist)
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
