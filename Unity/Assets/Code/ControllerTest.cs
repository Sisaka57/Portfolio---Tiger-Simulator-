using UnityEngine;
using System.Collections;
using Rewired;

public class ControllerTest : MonoBehaviour {

	public int Speed = 1;
	private float Normalize = 0.01f;
	private int playerID = 0;
	private Player player;

	private Vector3 thisInput = Vector3.zero;

	// Use this for initialization
	private void Awake ()
	{
		player = ReInput.players.GetPlayer (playerID);
	}

	// Update is called once per frame
	void Update () {
		thisInput.x = (player.GetAxis ("Move Horizontal") * Normalize) * Speed;
		thisInput.z = (player.GetAxis ("Move Vertical") * Normalize) * Speed;
		transform.Translate (thisInput);
	}
}
