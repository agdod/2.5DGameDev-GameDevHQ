using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
	[SerializeField] private bool _arrived = true;
	private MovingPlatform _movingPlatform;
	private bool _liftActivated = false;

	private void Start()
	{
		_movingPlatform = GetComponent<MovingPlatform>();
		if (_movingPlatform == null)
		{
			Debug.Log("Moving Platfrom Script not attached");
		}
	}

	private void FixedUpdate()
	{
		if (!_arrived)
		{
			_movingPlatform.MovePlatfrom();
			if (transform.position == _movingPlatform.TargetPosition)
			{
				_arrived = true;
			}
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player" && !_liftActivated)
		{
			
			{
				Debug.LogError("Entering Elevator");
				_liftActivated = true;
				CallElevator();
			}
			
		}
	}
	
	public void CallElevator()
	{
		_arrived = false;
		_liftActivated = false;
	}
}



