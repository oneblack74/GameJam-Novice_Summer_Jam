using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Activable))]
public class DoorActivable : MonoBehaviour, IActivable
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

    public void Activate()
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
}
