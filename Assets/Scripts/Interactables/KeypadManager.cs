using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeypadManager : MonoBehaviour
{
    [SerializeField] private string code;
    [SerializeField] private TextMeshProUGUI screen;
    [SerializeField] private GameObject targetValideCode;
    [SerializeField] private KeyPadInteractable keyPadInteractable;
    private string input;

    void Start()
    {
        input = "";
    }

    public void AddNumber(string number)
    {
        if (input.Length < code.Length)
        {
            input += number;
            screen.text = input;
        }
    }

    public void RemoveNumber()
    {
        if (input.Length > 0)
        {
            input = input.Remove(input.Length - 1);
            screen.text = input;
        }
    }

    public void ValidateCode()
    {
        if (input == code)
        {
            targetValideCode.GetComponent<IActivable>().Activate();
            keyPadInteractable.ExitView();
        }
        else
        {
            input = "";
            screen.text = input;
        }

    }


}
