using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class FlashlightInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private PlayerController player;
    public void Interact()
    {
        player.UnlockUVLight();
        gameObject.SetActive(false);
    }
}
