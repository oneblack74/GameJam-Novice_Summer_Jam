using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PadlockManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] codeTmPro;
    [SerializeField] private string code;
    [SerializeField] private PadlockInteractable padlockInteractable;
    [SerializeField] private GameObject activable;

    private string[] currentCode = new string[4] { "0", "0", "0", "0" };

    public void UpdateCodeTmPro()
    {
        for (int i = 0; i < codeTmPro.Length; i++)
        {
            codeTmPro[i].text = currentCode[i];
        }
    }

    public void TestCode()
    {
        string currentCodeString = string.Join("", currentCode);
        if (currentCodeString == code)
        {
            activable.layer = LayerMask.NameToLayer("InteractableObject");
            padlockInteractable.ExitViewAndDestroy();
        }
    }

    public void NumberPlus(int index)
    {
        Debug.Log("Plus");
        int number = int.Parse(currentCode[index]);
        number++;
        if (number > 9)
        {
            number = 0;
        }
        currentCode[index] = number.ToString();
        UpdateCodeTmPro();
        TestCode();
    }

    public void NumberMoins(int index)
    {
        Debug.Log("Moins");
        int number = int.Parse(currentCode[index]);
        number--;
        if (number < 0)
        {
            number = 9;
        }
        currentCode[index] = number.ToString();
        UpdateCodeTmPro();
        TestCode();
    }
}
