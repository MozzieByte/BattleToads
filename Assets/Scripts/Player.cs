using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : NetworkBehaviour
{
    public static Player LocalInstance { get; private set; }

    public static event EventHandler OnPlayerSpawned;

    [SerializeField] private GameObject cameraHolder;
    [SerializeField] private float speed = 20.0f;

    private Rigidbody2D rb2d;
    private Vector2 movementVector;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }
    public override void OnStartAuthority()
    {
        LocalInstance = this;
        cameraHolder.SetActive(true);

        OnPlayerSpawned?.Invoke(this, EventArgs.Empty);
    }

    public void MoveCallback(InputAction.CallbackContext context)
    {
        if (!isLocalPlayer)
            return;

        movementVector = context.ReadValue<Vector2>();
        //rb2d.MovePosition(inputVector);
    }
    public void Move()
    {
        Vector2 currentPos = rb2d.position;

        Vector2 adjustedMovement = movementVector * speed;

        Vector2 newPos = currentPos + adjustedMovement * Time.fixedDeltaTime;

        rb2d.MovePosition(newPos);
    }

    private void FixedUpdate()
    {
        Move();
    }
}
