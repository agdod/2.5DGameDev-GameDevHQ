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
			Debug.Log("triggered with : " + other);
			_uiManager.UpdateCoins(_coinValue);
			other.GetComponent<Player>().UpdateCoins(_coinValue);
			this.gameObject.SetActive(false);
		}
	}
}
