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
    [SerializeField] private Button nextPage;
    [SerializeField] private Button previousPage;
    [SerializeField] private GameObject notePad;
    private bool isNotePadOpen = false;

    private string[] notePadData;
    private int numPage = 0;
    [SerializeField] private int nbPage = 3;

    void Awake()
    {
        gameManager = GameManager.Instance;
        gameManager.inputs.actions["OpenNotePad"].performed += OpenNotePad;
        for (int i = 0; i < nbPage; i++)
        {
            notePadData[i] = "";
        }
        inputField.text = notePadData[0];
    }

    private void OpenNotePad(InputAction.CallbackContext context)
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

    public void CloseNotePad()
    {
        isNotePadOpen = false;
        notePad.SetActive(isNotePadOpen);
        notePadData[numPage] = inputField.text;
    }

    public void NextPage()
    {
        notePadData[numPage] = inputField.text;
        numPage++;
        if (numPage >= notePadData.Length)
        {
            numPage = 0;
            nextPage.gameObject.SetActive(false);
            previousPage.gameObject.SetActive(true);
        }
        inputField.text = notePadData[numPage];
    }

    public void PreviousPage()
    {
        notePadData[numPage] = inputField.text;
        numPage--;
        if (numPage < 0)
        {
            numPage = notePadData.Length - 1;
            previousPage.gameObject.SetActive(false);
            nextPage.gameObject.SetActive(true);
        }
        inputField.text = notePadData[numPage];
    }
}
