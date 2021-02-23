using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFalling : MonoBehaviour
{
	// platform under scene whihc trigger live lost when player falls through

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			if (other.TryGetComponent(out Player player))
			{
				player.UpdateLives();
			}
		}
	}
}
