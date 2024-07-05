using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotePadeInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private NotePadManager notePadManager;
    public void Interact()
    {
        notePadManager.ActiveNotePad();
        gameObject.SetActive(false);
    }
}
