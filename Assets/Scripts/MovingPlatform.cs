using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform _targetA, _targetB;
    [SerializeField] private float _speed = 1.0f;
	private Vector3 _targetPos;

	private void Start()
	{
		_targetPos = _targetB.position;
	}

	private void FixedUpdate()
	{
		
		transform.position = Vector3.MoveTowards(transform.position, GetTarget(), _speed * Time.deltaTime);
	}

	Vector3 GetTarget()
	{
		Vector3 currentPos = transform.position;
		 

		
		if (currentPos == _targetA.position)
		{
			_targetPos = _targetB.position;
		} 
		else if (currentPos == _targetB.position)
		{
			_targetPos = _targetA.position;
		}
		return _targetPos;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			other.gameObject.transform.parent = this.transform;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			other.gameObject.transform.parent = null;
		}
	}
}
