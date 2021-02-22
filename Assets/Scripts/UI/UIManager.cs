using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField] private TMP_Text _coinTxt;
	private int _coins;


	public void UpdateCoins(int amount)
	{
		_coins += amount;
		_coinTxt.text = "Coins :" + _coins.ToString();
	}
}
