using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class AIMovement : MonoBehaviour
{
	[Header("AI")]
	[Tooltip("Move speed of the character in m/s")]
	public float MoveSpeed = 4.0f;
	[Tooltip("Sprint speed of the character in m/s")]
	public float SprintSpeed = 6.0f;
	[Tooltip("Rotation speed of the character")]
	public float RotationSpeed = 1.0f;
	[Tooltip("Acceleration and deceleration")]
	public float SpeedChangeRate = 10.0f;


	[Header("AI Grounded")]
	[Tooltip("If the character is grounded or not. Not part of the CharacterController built in grounded check")]
	public bool Grounded = true;
	[Tooltip("Useful for rough ground")]
	public float GroundedOffset = -0.14f;
	[Tooltip("The radius of the grounded check. Should match the radius of the CharacterController")]
	public float GroundedRadius = 0.5f;
	[Tooltip("What layers the character uses as ground")]
	public LayerMask GroundLayers;

	[Tooltip("The character uses its own gravity value. The engine default is -9.81f")]
	public float Gravity = -15.0f;

	private CharacterController _controller;
	private float _speed;
	private float _terminalVelocity = 53.0f;
	private float _rotationVelocity;
	private float _verticalVelocity;

	// Start is called before the first frame update
	private void Start()
	{
		//_playerAnimationContoller = GetComponent<PlayerAnimationContoller>();
		_controller = GetComponent<CharacterController>();
	}

    private void Update()
    {
		CheckGravity();

	}

    private void LateUpdate()
    {
		GroundedCheck();

	}

	private void CheckGravity()
	{
		if (Grounded)
		{
			// stop our velocity dropping infinitely when grounded
			if (_verticalVelocity < 0.0f)
			{
				_verticalVelocity = -2f;
			}

		}
		

		// apply gravity over time if under terminal (multiply by delta time twice to linearly speed up over time)
		if (_verticalVelocity < _terminalVelocity)
		{
			_verticalVelocity += Gravity * Time.deltaTime;
		}
	}


	private void GroundedCheck()
	{
		// set sphere position, with offset
		Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - GroundedOffset, transform.position.z);
		Grounded = Physics.CheckSphere(spherePosition, GroundedRadius, GroundLayers, QueryTriggerInteraction.Ignore);
	}

	public void Move(Vector2 moveInput, bool sprint)
	{
		// set target speed based on move speed, sprint speed and if sprint is pressed
		float targetSpeed = sprint ? SprintSpeed : MoveSpeed;

		// a simplistic acceleration and deceleration designed to be easy to remove, replace, or iterate upon

		// note: Vector2's == operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is no input, set the target speed to 0
		if (moveInput == Vector2.zero) targetSpeed = 0.0f;

		// a reference to the players current horizontal velocity
		float currentHorizontalSpeed = new Vector3(_controller.velocity.x, 0.0f, _controller.velocity.z).magnitude;

		float speedOffset = 0.1f;
		float inputMagnitude = 1f;

		// accelerate or decelerate to target speed
		if (currentHorizontalSpeed < targetSpeed - speedOffset || currentHorizontalSpeed > targetSpeed + speedOffset)
		{
			// creates curved result rather than a linear one giving a more organic speed change
			// note T in Lerp is clamped, so we don't need to clamp our speed
			_speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude, Time.deltaTime * SpeedChangeRate);

			// round speed to 3 decimal places
			_speed = Mathf.Round(_speed * 1000f) / 1000f;
		}
		else
		{
			_speed = targetSpeed;
		}

		// normalise input direction
		Vector3 inputDirection = new Vector3(moveInput.x, 0.0f, moveInput.y).normalized;

		// note: Vector2's != operator uses approximation so is not floating point error prone, and is cheaper than magnitude
		// if there is a move input rotate player when the player is moving
		if (moveInput != Vector2.zero)
		{
			// move
			inputDirection = transform.right * moveInput.x + transform.forward * moveInput.y;
		}
		//Pass the input to the blend tree for the movement
		//_playerAnimationContoller.MovementAnimation(x: _input.move.x, y: _input.move.y, sprintSpeed: SprintSpeed, speed: _speed, moveSpeed: MoveSpeed);


		// move the player
		_controller.Move(inputDirection.normalized * (_speed * Time.deltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
		// rotate the player
		AIRotation(moveInput.x);
	}

	private void AIRotation(float rotationInput )
	{
		_rotationVelocity = rotationInput * RotationSpeed * Time.deltaTime;
		// Apply the rotation velocity to the character's rotation
		transform.Rotate(Vector3.up * _rotationVelocity);
		
	}
}
