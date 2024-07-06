using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPadInteractable : MonoBehaviour, IInteractable
{

    [SerializeField] private Transform targetPosition;
    [SerializeField] private Transform cameraposition;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject canvas;
    [SerializeField] private float transitionDuration = 1.0f;
    private GameManager gameManager;
    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private Coroutine currentCoroutine;
    private bool isFocus = false;

    private void Start()
    {
        gameManager = GameManager.Instance;
    }

    [ContextMenu("Interact")]
    public void Interact()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        initialPosition = cameraposition.position;
        initialRotation = cameraposition.rotation;
        canvas.SetActive(true);
        currentCoroutine = StartCoroutine(MoveCamera(initialPosition, initialRotation, targetPosition.position, targetPosition.rotation, true));

        isFocus = !isFocus;
    }

    public void ExitView()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        playerController.BlockPlayerToggle();
        crossHair.SetActive(true);
        gameManager.LockCursor(true);
        canvas.SetActive(false);
        currentCoroutine = StartCoroutine(MoveCamera(targetPosition.position, targetPosition.rotation, initialPosition, initialRotation));
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
        }
        crossHair.SetActive(false);
        gameManager.LockCursor(false);
    }
}
