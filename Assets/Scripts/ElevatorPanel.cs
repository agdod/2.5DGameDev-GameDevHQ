using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanel : MonoBehaviour
{
	// Detect trigger collsion
	// Check for player
	// if e key is pressed 
	// turn the light green

	[SerializeField] private MeshRenderer _callButton;
	[SerializeField] private int _cost = 8;
	[SerializeField] private ElevatorController _elevator;
	private bool _elevatorCalled;

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			if (other.TryGetComponent(out Player player))
			{
				if (Input.GetKeyDown(KeyCode.E) && player.Coins >= _cost)
				{
					if(_elevatorCalled)
					{
						_callButton.material.color = Color.red;
					}
					_callButton.material.color = Color.green;
					_elevator.CallElevator();
				}
			}
		}
	}

}
