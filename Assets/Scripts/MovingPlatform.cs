using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	[SerializeField] private Transform _targetA, _targetB;
	[SerializeField] private float _speed = 3.0f;
	[SerializeField] private bool _autoMove; // platfrom moves between ppoints automatically
	private Vector3 _targetPos;

	public Vector3 TargetPosition
	{
		get { return _targetPos; }
	}

	private void Start()
	{
		_targetPos = _targetA.position;
	}

	private void FixedUpdate()
	{
		if (_autoMove)
		{
			MovePlatfrom();
		}
	}

	public void MovePlatfrom()
	{
		transform.position = Vector3.MoveTowards(transform.position, GetTarget(), _speed * Time.deltaTime);
	}

	private Vector3 GetTarget()
	{
		Vector3 currentPos = transform.position;

		if (currentPos == _targetA.position)
		{
			_targetPos = _targetB.position;
			 if (!_autoMove)
				Debug.LogError("Target is : " + _targetB.name);
		}
		else if (currentPos == _targetB.position)
		{
			_targetPos = _targetA.position;
			if (!_autoMove)
				Debug.LogError("Target is : " + _targetA.name);
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
