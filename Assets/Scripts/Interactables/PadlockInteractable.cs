using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class PadlockInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform targetPosition;
    [SerializeField] private Transform cameraposition;
    [SerializeField] private Transform anchorPositionCamera;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject crossHair;
    [SerializeField] private GameObject canvas;
    [SerializeField] private NotePadManager notePadManager;
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
        notePadManager.CanActive = false;
        initialPosition = anchorPositionCamera.position;
        initialRotation = anchorPositionCamera.rotation;
        canvas.SetActive(true);
        playerController.BlockPlayerToggle();
        currentCoroutine = StartCoroutine(MoveCamera(initialPosition, initialRotation, targetPosition.position, targetPosition.rotation, true));

        isFocus = !isFocus;
    }

    public void ExitViewAndDestroy()
    {
        ExitView();
        StartCoroutine(WaitForDestroy());
    }

    IEnumerator WaitForDestroy()
    {
        yield return new WaitForSeconds(transitionDuration + 0.1f);
        gameObject.SetActive(false);
    }

    [ContextMenu("ExitView")]
    public void ExitView()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }

        crossHair.SetActive(true);
        canvas.SetActive(false);
        currentCoroutine = StartCoroutine(MoveCamera(targetPosition.position, targetPosition.rotation, anchorPositionCamera.position, anchorPositionCamera.rotation));
        isFocus = !isFocus;

    }

    private IEnumerator MoveCamera(Vector3 startPos, Quaternion startRot, Vector3 endPos, Quaternion endRot, bool delock = false)
    {
        if (delock)
        {
            crossHair.SetActive(false);
            gameManager.LockCursor(false);
        }
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
            crossHair.SetActive(false);
            gameManager.LockCursor(false);
        }
        else
        {
            notePadManager.CanActive = true;
            gameManager.LockCursor(true);
            playerController.BlockPlayerToggle();
            cameraposition.localPosition = Vector3.zero;
        }
    }
}
