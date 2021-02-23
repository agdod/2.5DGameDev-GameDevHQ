using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
	[SerializeField] private TMP_Text _coinTxt, _livesTxt;

	public void UpdateCoins(int amount)
	{
		_coinTxt.text = "Coins : " + amount.ToString();
	}

	public void UpdateLives(int lives)
	{
		_livesTxt.text = "Lives : " + lives.ToString();
	}
}
