using Mirror;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : NetworkBehaviour
{
    public static PlayerController LocalInstance { get; private set; }

    public static event EventHandler OnPlayerSpawned;

    [SerializeField] private GameObject cameraHolder;

    private CharacterController controller;
	private Vector2 moveInput;

    private float Current_Speed = 3.5f;
	private float Max_Speed = 3.5f;
	private float Base_Speed = 3.5f;
	private float Sprint_Speed = 6.5f;
	private bool Is_Sprinting = false;

    public override void OnStartAuthority()
    {
        LocalInstance = this;
        cameraHolder.SetActive(true);

        OnPlayerSpawned?.Invoke(this, EventArgs.Empty);
    }

    void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	private void Update()
	{
        if (!isOwned)
            return;

        if (Is_Sprinting)
			Max_Speed = Sprint_Speed;
		else
			Max_Speed = Base_Speed;

		MovePlayer();
	}

	public void OnMove(InputAction.CallbackContext context)
	{
        moveInput = context.ReadValue<Vector2>();
	}

	public void OnSprint(InputAction.CallbackContext context)
	{
		Is_Sprinting = (context.ReadValue<float>() == 1);
	}

	public void MovePlayer()
	{
        Vector3 moveDirection = new Vector3(moveInput.x, moveInput.y, 0);

		if (moveDirection.x != 0 || moveDirection.y != 0)
		{
			if (Current_Speed < Max_Speed)
			{
				Current_Speed += 0.1f;
			}
			if (Current_Speed > Max_Speed)
			{
				Current_Speed = Max_Speed;
			}
		}
		else
		{
			if (Current_Speed > 0)
			{
				Current_Speed -= 0.1f;
			}
		}


		controller.Move(transform.TransformDirection(moveDirection) * Current_Speed * Time.deltaTime);
	}
}
