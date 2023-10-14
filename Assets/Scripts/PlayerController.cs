using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
	private CharacterController controller;
	private Vector2 moveInput;
	private float Current_Speed = 3.5f;
	private float Max_Speed = 3.5f;
	private float Base_Speed = 3.5f;
	private float Sprint_Speed = 6.5f;
	private bool Is_Sprinting = false;

	[SerializeField]
	private SpriteRenderer renderer;
	[SerializeField]
	private Animator animator;

	void Start()
	{
		controller = GetComponent<CharacterController>();
	}

	private void Update()
	{

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
			if (moveDirection.x > 0)
			{
				renderer.flipX = true;
			}
			else if (moveDirection.x < 0)
			{
				renderer.flipX = false;
			}

			if (Current_Speed < Max_Speed)
			{
				Current_Speed += 0.1f;
			}
			if (Current_Speed > Max_Speed)
			{
				Current_Speed = Max_Speed;
			}
			animator.SetBool("IsMoving", true);
		}
		else
		{
			if (Current_Speed > 0)
			{
				Current_Speed -= 0.1f;
			}
			animator.SetBool("IsMoving", false);
		}


		controller.Move(transform.TransformDirection(moveDirection) * Current_Speed * Time.deltaTime);
	}
}
