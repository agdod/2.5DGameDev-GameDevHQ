using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private UIManager _uiManager;
	[SerializeField] private int _coinValue;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			
			if (other.TryGetComponent(out Player player))
			{
				player.UpdateCoins(_coinValue);
			}
			Debug.Log("triggered with : " + other);
			_uiManager.UpdateCoins(_coinValue);
			
			this.gameObject.SetActive(false);
		}
	}
}
