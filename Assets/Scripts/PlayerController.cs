using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameManager manager;
    private InputAction moveAction;
    private InputAction lookAction;

    [SerializeField] private Camera camera;

    [SerializeField] private float speed = 0.12f;
    private Vector2 moveValue;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.GetInstance();
        moveAction = manager.GetInputs.actions["Move"];
        lookAction = manager.GetInputs.actions["Look"];
    }

    private void HandleCamRotation()
    {
        float mouseXRotation;
    }

    void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        gameObject.transform.position += new Vector3(moveValue.x, 0, moveValue.y) * speed;

    }
}
