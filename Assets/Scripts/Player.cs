using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
	// Managers
	[SerializeField] private UIManager _uiManager;

	//Player 
	[SerializeField] private CharacterController _controller;
	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private float _gravityMod = 1.0f;
	[SerializeField] private float _jumpHeight = 15.0f;
	[SerializeField] private int _lives = 3;
	[SerializeField] private float _pushForce = 2.0f;
	[SerializeField] private Transform _startingPos;

	[SerializeField] private int _coins;

	private Vector3 _direction;
	private Vector3 _velocity;

	private float _yVelocity; // Caching y velocity between frames
	private bool _canDoubleJump;
	private bool _livesTriggered;
	private bool _canWallJump = false;
	private Vector3 _wallJumpDirection;
	public int Coins
	{
		get { return _coins; }
	}

	private void Start()
	{
		_controller = GetComponent<CharacterController>();
		if (!_controller)
		{
			Debug.Log("Character controller not found.");
		}
		_uiManager.UpdateLives(_lives);
		transform.position = _startingPos.position;
	}
	// Get horizontal input
	// define direction based on input
	// Move based on direction

	private void Update()
	{
		if (_controller.enabled == true)
		{
			if (_controller != null)
			{
				PlayerMovement();
			}
		}
	}

	private void PlayerMovement()
	{
		float horizontal = Input.GetAxis("Horizontal");

		// Apply Gravity effect
		if (_controller.isGrounded)
		{
			_canWallJump = false;
			_canDoubleJump = false;
			_direction = new Vector3(horizontal, 0, 0);
			_velocity = _direction * _speed;
			// Enable jump 
			if (Input.GetKey(KeyCode.Space))
			{
				_yVelocity = _jumpHeight;
				_canDoubleJump = true;
			}
		}
		else
		{
			// Check for Jump ability
			if (Input.GetKeyDown(KeyCode.Space))
			{
				// Check for jump type.
				// Wall jump first then double jump checking
				if (_canWallJump)
				{
					_canDoubleJump = false;
					_yVelocity = _jumpHeight;
					_velocity = _wallJumpDirection * _speed;
				}
				if (_canDoubleJump)
				{
					_yVelocity += _jumpHeight;
					_canDoubleJump = false;
				}

			}
			// else fall to ground.
			else
			{
				_yVelocity -= _gravityMod;
			}
		}
		_velocity.y = _yVelocity;
		_controller.Move(_velocity * Time.deltaTime);
	}

	private void OnControllerColliderHit(ControllerColliderHit hit)
	{
		// Wall jumping
		// Hit the wall and not grounded
		if (!_controller.isGrounded && hit.transform.tag == "Wall")
		{
			// Can wall jump
			_canWallJump = true;
			// Wall jump direction
			_wallJumpDirection = hit.normal;
		}

		// Else has hit object , check if pushable object
		// Check for rigidbody
		if (hit.collider.TryGetComponent (out Rigidbody body) ){
			if (Pushable(hit, body))
			{
				// Calculate push direction from move direction
				// push along x never up or down, only 2.5D game so no z-axis
				Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);

				// multipy the push velcity by player velcoity
				// Apply the push
				//_pushForce += _velocity.x;
				body.velocity = pushDir * _pushForce;
			}
			//body = hit.collider.attachedRigidbody;
		}	
	}

	private bool Pushable(ControllerColliderHit hit, Rigidbody body)
	{
		if (hit.collider.tag != "Moveable")
		{
			return false;
		}
		// If object has no rigidbody return
		if (body == null || body.isKinematic)
		{
			return false;
		}
		// Dont push object below player.
		if (hit.moveDirection.y < -0.3f)
		{
			return false;
		}
		return true;
	}

	void ResetPosition()
	{
		_controller.enabled = false;
		StartCoroutine(ReEnableCharController());
		transform.position = _startingPos.position;
		_livesTriggered = false;
	}

	IEnumerator ReEnableCharController()
	{
		yield return new WaitForSeconds(0.1f);
		_controller.enabled = true;
	}

	public void UpdateLives()
	{
		Debug.Log("Updating lives.");
		if (_livesTriggered == false)
		{
			_lives--;
			_livesTriggered = true;
		}

		//If player has zero lives restart
		if (_lives == 0)
		{
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
		else
		{
			ResetPosition();
		}
		_uiManager.UpdateLives(_lives);

	}

	public void UpdateCoins(int amount)
	{
		_coins += amount;
		_uiManager.UpdateCoins(_coins);
	}
}
