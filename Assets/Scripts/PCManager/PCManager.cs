using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PCManager : MonoBehaviour
{
    [SerializeField] private string Screen1Pseudo;
    [SerializeField] private string Screen1Password;
    [SerializeField] private string CodeFinalPassword;
    [SerializeField] private TMP_InputField Screen1PseudoInput;
    [SerializeField] private TMP_InputField Screen1PasswordInput;
    [SerializeField] private GameObject[] screens;

    [SerializeField] private TMP_InputField codeFinalInputField;
    [SerializeField] private GameObject codeFinalPasswordPage;

    [SerializeField] private GameObject feuilleImpression;

    public void Screen1Connexion()
    {
        if (Screen1PseudoInput.text == Screen1Pseudo && Screen1PasswordInput.text == Screen1Password)
        {
            screens[1].SetActive(false);
            screens[2].SetActive(true);
        }
    }

    public void CodeFinalPasswordOpen()
    {
        if (codeFinalInputField.text == CodeFinalPassword)
        {
            codeFinalPasswordPage.SetActive(true);
        }
    }

    public void Print()
    {
        feuilleImpression.GetComponent<IActivable>().Activate();
        screens[2].transform.GetChild(1).GetComponent<Button>().interactable = false;
    }

}
