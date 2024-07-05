using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class ButtonInteractable : MonoBehaviour, IInteractable
{
    public bool Interact()
    {
        throw new System.NotImplementedException();
    }
}