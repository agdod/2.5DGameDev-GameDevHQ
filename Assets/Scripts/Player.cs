using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[SerializeField] private CharacterController _controller;
	[SerializeField] private float _speed = 5.0f;
	[SerializeField] private float _gravityMod = 1.0f;
	[SerializeField] private float _jumpHeight= 15.0f;

	private float _yVelocity; // Caching y velocity between frames
	private bool _canDoubleJump;
	private int _coins;

	private void Start()
	{
		_controller = GetComponent<CharacterController>();
		if (!_controller)
		{
			Debug.Log("Character controller not found.");
		}

	}


	// Get horizontal input
	// define direction based on input
	// Move based on direction

	private void Update()
	{
		if (_controller != null)
		{
			float horizontal = Input.GetAxis("Horizontal");
			Vector3 direction = new Vector3(horizontal,0,0);
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

	public void UpdateCoins(int amount)
	{
		_coins += amount;
	}
}
