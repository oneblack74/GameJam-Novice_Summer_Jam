using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[RequireComponent(typeof(Inventory))]
public class PlayerController : MonoBehaviour
{
    private Camera mainCamera;
    private GameManager manager;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction scrollAction;
    private CharacterController characterController;
    private Inventory inventory;
    [SerializeField] private GameObject flashlight;

    private int selectedItem;
    private bool isMoving = false;
    private bool blockPlayer = false;

    [SerializeField] private float speed = 0.12f;
    [SerializeField] private float mouseSensitivity = 0.1f;
    public float MouseSensitivity
    {
        get { return mouseSensitivity; }
        set { mouseSensitivity = value; }
    }
    [SerializeField] private float upDownLookRange = 80f;
    [SerializeField] private float grabRange = 100f;
    [SerializeField] private GameObject inventoryUI;
    private Vector2 moveValue;
    private Vector2 lookValue;
    private Vector2 scrollValue;
    private Vector3 currentMovement;
    private float mouseVerticalRotation = 0;

    private RaycastHit lookingAt;
    private bool lookAtSomething;
    private bool hasUVLight = false;
    private bool flashing = false;


    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.Instance;
        mainCamera = Camera.main;
        characterController = GetComponent<CharacterController>();
        moveAction = manager.GetInputs.actions["Move"];
        lookAction = manager.GetInputs.actions["Look"];
        scrollAction = manager.GetInputs.actions["MouseScroll"];
        manager.LockCursor(true);
        manager.inputs.actions["Use"].performed += Use;
        manager.inputs.actions["Flashlight"].performed += UseFlashlight;
        inventory = GetComponent<Inventory>();
        mouseSensitivity = SaveData.Instance.data.mouseSensitivity;
    }

    [ContextMenu("AddBook")]
    public void AddBook()
    {
        AddItem(manager.ConvertIdToItem(1));
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
        int layerMask = LayerMask.GetMask("InteractableObject") | LayerMask.GetMask("OclusionMask");
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.TransformDirection(Vector3.forward), out lookingAt, grabRange, layerMask))
        {
            Interactable found = lookingAt.transform.GetComponent<Interactable>();
            if (found != null)
            {
                lookAtSomething = true;
                found.ToggleOutline();
            }
            else
            {
                lookAtSomething = false;
            }
        }
        else
        {
            lookAtSomething = false;
        }
    }

    private void Use(InputAction.CallbackContext context)
    {
        if (lookAtSomething)
        {
            lookingAt.transform.GetComponent<IInteractable>().Interact();
        }
    }

    private void UseFlashlight(InputAction.CallbackContext context)
    {
        if (!hasUVLight)
        {
            return;
        }
        SoundEffectManager.Instance.PlaySoundEffect("SE_ActiveFlashlight");
        if (flashing)
        {
            flashlight.transform.localPosition = new Vector3(0, -1000, 0);
            flashing = false;
        }
        else
        {
            flashlight.transform.localPosition = new Vector3(0, 0, 0);
            flashing = true;
        }
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
            inventory.RemoveItem(res);
        }
    }

    public void DrawInventory()
    {
        int nbItems = inventory.GetNumberOfItems();
        if (nbItems == 0)
        {
            inventoryUI.SetActive(false);
            return;
        }
        else
        {
            inventoryUI.SetActive(true);
        }
        // Middle Image
        inventoryUI.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>().sprite = GetSelectedItem().GetIcon;
        // Down Image
        if (nbItems <= 2)
        {
            inventoryUI.transform.GetChild(2).gameObject.SetActive(false);
        }
        else
        {
            inventoryUI.transform.GetChild(2).gameObject.SetActive(true);
            inventoryUI.transform.GetChild(2).transform.GetChild(0).GetComponent<Image>().sprite = GetSelectedItem(-1).GetIcon;
        }
        // Up Image
        if (nbItems <= 1)
        {
            inventoryUI.transform.GetChild(0).gameObject.SetActive(false);
        }
        else
        {
            inventoryUI.transform.GetChild(0).gameObject.SetActive(true);
            inventoryUI.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = GetSelectedItem(1).GetIcon;
        }
    }

    public ItemDefinition GetSelectedItem(int offset = 0)
    {
        if (selectedItem + offset >= inventory.GetNumberOfItems())
        {
            return inventory.CheckItem(0);
        }
        if (selectedItem + offset < 0)
        {
            return inventory.CheckItem(inventory.GetNumberOfItems() - 1);
        }
        return inventory.CheckItem(selectedItem + offset);
    }

    public void BlockPlayerToggle()
    {
        if (blockPlayer)
        {
            blockPlayer = false;
            manager.inputs.actions["Use"].performed += Use;
        }
        else
        {
            blockPlayer = true;
            manager.inputs.actions["Use"].performed -= Use;
        }
    }

    public void HandleItemSelection()
    {
        scrollValue = scrollAction.ReadValue<Vector2>();
        if (scrollValue.y >= 100)
        {
            selectedItem++;
        }
        else if (scrollValue.y <= -100)
        {
            selectedItem--;
        }
        if (selectedItem >= inventory.GetNumberOfItems())
        {
            selectedItem = 0;
        }
        else if (selectedItem < 0)
        {
            selectedItem = inventory.GetNumberOfItems() - 1;
        }
    }

    public void UnlockUVLight()
    {
        hasUVLight = true;
    }

    public IEnumerator Die()
    {
        blockPlayer = true;
        mainCamera.GetComponent<CameraShake>().start = true;
        yield return new WaitForSeconds(1);
        transform.GetChild(0).GetComponent<SphereCollider>().enabled = true;
        transform.GetChild(0).GetComponent<Rigidbody>().useGravity = true;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<CharacterController>().enabled = false;
        SaveData.Instance.data.deathCounter++;
        SaveData.Instance.Save();
    }

    void Update()
    {
        if (!blockPlayer)
        {
            HandleCamRotation();
            LookAtInteratable();
            moveValue = moveAction.ReadValue<Vector2>();
            HandleItemSelection();
        }
        DrawInventory();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.name == "TriggerEndDemo")
        {
            Debug.Log("Fin de la démo");
        }
    }

    void FixedUpdate()
    {
        if (!blockPlayer)
        {
            HandleMovement();
        }
    }

    public Inventory Inv
    {
        get { return inventory; }
    }
}
