using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class ButtonInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject linkActivable;

    public void Interact()
    {
        linkActivable.GetComponent<IActivable>().Activate();
    }
}