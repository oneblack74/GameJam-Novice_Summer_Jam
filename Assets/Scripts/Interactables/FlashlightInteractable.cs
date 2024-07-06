using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class FlashlightInteractable : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.Instance.Player.UnlockUVLight();
        InfosManager.Instance.OpenInfoPanel("GetFlashLight");
        gameObject.SetActive(false);
    }
}
