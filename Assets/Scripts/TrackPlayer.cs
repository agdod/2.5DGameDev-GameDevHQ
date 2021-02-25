using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;

	private void Update()
	{
		Vector3 track = transform.position;
		track.x = _player.transform.position.x;
		transform.position = track;
	}
}
