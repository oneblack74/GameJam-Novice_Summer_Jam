using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    private GameManager manager;
    private InputAction moveAction;
    private InputAction lookAction;
    private CharacterController characterController;


    private bool isMoving = false;

    [SerializeField] private float speed = 0.12f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float upDownLookRange = 80f;
    private Vector2 moveValue;
    private Vector2 lookValue;
    private Vector3 currentMovement;
    private float mouseVerticalRotation = 0;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.GetInstance();
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        moveAction = manager.GetInputs.actions["Move"];
        lookAction = manager.GetInputs.actions["Look"];
        manager.LockCursor(true);
    }

    private void HandleCamRotation()
    {
        lookValue = lookAction.ReadValue<Vector2>();
        float mouseXRotation = lookValue.x * mouseSensitivity;
        transform.Rotate(0, mouseXRotation, 0);

        mouseVerticalRotation -= lookValue.y * mouseSensitivity;
        mouseVerticalRotation = Mathf.Clamp(mouseVerticalRotation, -upDownLookRange, upDownLookRange);
        mainCamera.transform.localRotation = Quaternion.Euler(mouseVerticalRotation, 0, 0);
    }

    private void HandleMovement()
    {
        float verticalSpeed = moveValue.y * speed;
        float horizontalSpeed = moveValue.x * speed;

        Vector3 horizontalMovement = new Vector3(horizontalSpeed, 0, verticalSpeed);
        horizontalMovement = transform.rotation * horizontalMovement;

        // Gravity

        currentMovement.x = horizontalMovement.x;
        currentMovement.z = horizontalMovement.z;

        characterController.Move(currentMovement * Time.deltaTime);

        isMoving = verticalSpeed != 0 || horizontalSpeed != 0;
    }

    void Update()
    {
        HandleCamRotation();
        moveValue = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }
}
