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
	[SerializeField] private float _jumpHeight= 15.0f;
	[SerializeField] private int _lives = 3;
	[SerializeField] private Transform _startingPos;

	[SerializeField] private int _coins;

	private float _yVelocity; // Caching y velocity between frames
	private bool _canDoubleJump;
	private bool _livesTriggered;

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
				float horizontal = Input.GetAxis("Horizontal");
				Vector3 direction = new Vector3(horizontal, 0, 0);
				Vector3 velocity = direction * _speed;

				// Apply Gravity effect
				if (_controller.isGrounded)
				{
					// Enable jump 
					if (Input.GetKey(KeyCode.Space))
					{
						_yVelocity = _jumpHeight;
						_canDoubleJump = true;
					}
				}
				else
				{
					// Check for double jump
					// Can only double jump , second jump if not grounded ie still in air
					if (Input.GetKeyDown(KeyCode.Space))
						if (_canDoubleJump)
						{
							_yVelocity += _jumpHeight;
							_canDoubleJump = false;
						}

					_yVelocity -= _gravityMod;
				}
				velocity.y = _yVelocity;
				_controller.Move(velocity * Time.deltaTime);
			}
		}
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
