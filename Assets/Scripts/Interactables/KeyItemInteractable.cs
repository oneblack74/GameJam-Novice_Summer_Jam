using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class KeyItemInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private int itemID;

    public void Interact()
    {
        GameManager.Instance.Player.AddItem(GameManager.Instance.ConvertIdToItem(itemID));
        gameObject.SetActive(false);
    }
}
