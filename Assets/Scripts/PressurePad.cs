using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePad : MonoBehaviour
{
	// Dectect moving box
	// When close to center disbale box rigidbody (or set to iskinemtic)
	// Change color of box to blue.

	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Moveable")
		{
			// check when close to center
			float distance = Vector3.Distance(transform.position, other.transform.position);
			if (distance < 0.5)
			{
				other.attachedRigidbody.isKinematic = true;
				if (other.TryGetComponent(out MeshRenderer mesh))
				{
					mesh.material.color = Color.blue;
				}
				// Remove the rigidBody componet to stop ontrigger being called.
				Destroy(other.attachedRigidbody);
				// Change color of pad to red
				MeshRenderer thisMesh = GetComponentInChildren<MeshRenderer>();
				if (thisMesh != null)
				{
					thisMesh.material.color = Color.red;
				}
			}


		}
	}
}
