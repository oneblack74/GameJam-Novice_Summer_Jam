using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KeypadManager : MonoBehaviour
{
    [SerializeField] private string code;
    [SerializeField] private TextMeshProUGUI screen;
    [SerializeField] private IInteractable targetValideCode;
    [SerializeField] private KeyPadInteractable keyPadInteractable;
    private string input;

    public void AddNumber(string number)
    {
        if (input.Length >= 4) return;
        input += number;
        screen.text = input;
    }

    public void RemoveNumber()
    {
        input = input.Remove(input.Length - 1);
        screen.text = input;
    }

    public void ValidateCode()
    {
        if (input == code)
        {
            targetValideCode.Interact();
            keyPadInteractable.ExitView();
        }
        else
        {
            input = "";
            screen.text = input;
        }

    }


}
