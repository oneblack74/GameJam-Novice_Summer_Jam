using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class PCInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private Transform cameraposition;
    [SerializeField] private float transitionDuration = 1.0f;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private NotePadManager notePadManager;
    [SerializeField] private GameObject[] screen;
    [SerializeField] private GameObject screenCross;
    private GameManager gameManager;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Coroutine currentCoroutine;
    private bool isFocus = false;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    public void Interact()
    {
        notePadManager.CanActive = false;
        initialPosition = cameraposition.position;
        initialRotation = cameraposition.rotation;
        screen[1].SetActive(true);
        screen[0].SetActive(false);
        screenCross.SetActive(true);
        currentCoroutine = StartCoroutine(MoveCamera(initialPosition, initialRotation, targetPosition.position, targetPosition.rotation, true));
        isFocus = !isFocus;
    }

    private IEnumerator MoveCamera(Vector3 startPos, Quaternion startRot, Vector3 endPos, Quaternion endRot, bool delock = false)
    {
        float elapsedTime = 0f;
        while (elapsedTime < transitionDuration)
        {
            cameraposition.position = Vector3.Lerp(startPos, endPos, elapsedTime / transitionDuration);
            cameraposition.rotation = Quaternion.Lerp(startRot, endRot, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        cameraposition.position = endPos;
        cameraposition.rotation = endRot;
        if (delock)
        {
            playerController.BlockPlayerToggle();
            crossHair.SetActive(false);
            gameManager.LockCursor(false);
        }
        else
        {
            notePadManager.CanActive = true;
        }
    }

    [ContextMenu("ExitView")]
    public void ExitView()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        playerController.BlockPlayerToggle();
        crossHair.SetActive(true);
        gameManager.LockCursor(true);
        screen[1].SetActive(false);
        screen[2].SetActive(false);
        screen[0].SetActive(true);
        screenCross.SetActive(false);
        currentCoroutine = StartCoroutine(MoveCamera(targetPosition.position, targetPosition.rotation, initialPosition, initialRotation));
        isFocus = !isFocus;
    }
}
