using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class ButtonInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject linkActivable = null;

    public void Interact()
    {
        if (!(linkActivable == null))
        {
            linkActivable.GetComponent<IActivable>().Activate();
        }
    }
}