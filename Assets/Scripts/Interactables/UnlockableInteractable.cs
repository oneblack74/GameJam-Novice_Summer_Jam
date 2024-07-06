using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class UnlockableInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] int unlockItem;
    [SerializeField] GameObject unlockedObject;
    private GameManager manager;

    void Start()
    {
        manager = GameManager.Instance;
    }

    public void Interact()
    {
        if (manager.Player.GetSelectedItem().GetID == unlockItem)
        {
            unlockedObject.layer = LayerMask.NameToLayer("InteractableObject");
            manager.Player.RemoveItem(manager.ConvertIdToItem(unlockItem));
            gameObject.SetActive(false);
        }
    }
}
