using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class DoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private float angleToOpen = 90f;
    [SerializeField] private bool isOpen = false;
    [SerializeField] private float duration = 1.0f;


    private Quaternion closedRotation;
    private Quaternion openRotation;
    private Coroutine currentCoroutine;

    private void Start()
    {
        closedRotation = transform.rotation;
        openRotation = closedRotation * Quaternion.Euler(0, angleToOpen, 0);
    }

    public void Interact()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        if (isOpen)
        {
            currentCoroutine = StartCoroutine(CloseDoor());
        }
        else
        {
            currentCoroutine = StartCoroutine(OpenDoor());
        }
        isOpen = !isOpen;
    }

    private IEnumerator OpenDoor()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(closedRotation, openRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = openRotation;
    }

    private IEnumerator CloseDoor()
    {
        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            transform.rotation = Quaternion.Lerp(openRotation, closedRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.rotation = closedRotation;
    }
}
