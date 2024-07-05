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
    private Inventory inventory;

    private bool isMoving = false;

    [SerializeField] private float speed = 0.12f;
    [SerializeField] private float mouseSensitivity = 1f;
    [SerializeField] private float upDownLookRange = 80f;
    [SerializeField] private float grabRange = 100f;
    private Vector2 moveValue;
    private Vector2 lookValue;
    private Vector3 currentMovement;
    private float mouseVerticalRotation = 0;

    private RaycastHit lookingAt;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        moveAction = manager.GetInputs.actions["Move"];
        lookAction = manager.GetInputs.actions["Look"];
        manager.LockCursor(true);
        manager.inputs.actions["Use"].performed += Use;
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

    private void LookAtInteratable()
    {
        int layerMask = 1 << 6;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out lookingAt, grabRange, layerMask))
        {
            lookingAt.transform.GetComponent<Interactable>().ToggleOutline();
        }
    }

    private void Use(InputAction.CallbackContext context)
    {
        lookingAt.transform.GetComponent<IInteractable>().Interact();
    }

    public void AddItem(ItemDefinition item)
    {
        inventory.AddItemFast(item, 1);
    }

    public void RemoveItem(ItemDefinition item)
    {
        int res = inventory.GetItemIndex(item.GetID);
        if (res != -1)
        {
            inventory.RemoveItem(res, 1);
        }
    }

    void Update()
    {
        HandleCamRotation();
        LookAtInteratable();
        moveValue = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        HandleMovement();
    }
}
