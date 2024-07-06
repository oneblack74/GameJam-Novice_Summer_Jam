using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{

    public void Interact()
    {
        Debug.Log("Interacting with door");
    }



}
