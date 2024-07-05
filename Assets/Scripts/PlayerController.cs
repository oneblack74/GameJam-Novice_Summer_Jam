using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private GameManager manager;
    private InputAction moveAction;


    private Vector3 moveValue;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameManager.GetInstance();
        moveAction = manager.GetInputs.actions["Move"];
    }

    void FixedUpdate()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        gameObject.transform.position = moveValue;
    }
}
