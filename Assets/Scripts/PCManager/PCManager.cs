using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PCManager : MonoBehaviour
{
    [SerializeField] private string Screen1Pseudo;
    [SerializeField] private string Screen1Password;
    [SerializeField] private TMP_InputField Screen1PseudoInput;
    [SerializeField] private TMP_InputField Screen1PasswordInput;
    [SerializeField] private GameObject[] screens;

    [SerializeField] private GameObject feuilleImpression;

    public void Screen1Connexion()
    {
        if (Screen1PseudoInput.text == Screen1Pseudo && Screen1PasswordInput.text == Screen1Password)
        {
            screens[1].SetActive(false);
            screens[2].SetActive(true);
        }
    }

    public void Print()
    {
        feuilleImpression.GetComponent<IActivable>().Activate();
    }

}
