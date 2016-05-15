using Rewired;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Require a character controller to be attached to the same game object
[RequireComponent(typeof(CharacterMotor))]
[AddComponentMenu("Character/FPS Input Controller")]

public class ControllerTest : MonoBehaviour
{
	private CharacterMotor motor;
	private int playerID = 0;
	private Player player;
	private Animator thisAnimator;
	private Vector3 thisInput = Vector3.zero;

	private void Awake()
	{
		motor = GetComponent<CharacterMotor>();
		player = ReInput.players.GetPlayer (playerID);
		thisAnimator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update()
	{
		// Get the input vector from input
		thisInput.x = player.GetAxis ("Move Horizontal");
		thisInput.z = player.GetAxis ("Move Vertical");
		motor.isSprint = player.GetButton ("Sprint");
		if (motor.grounded) {
			if (thisInput != Vector3.zero) {
				// Get the length of the directon vector and then normalize it
				// Dividing by the length is cheaper than normalizing when we already have the length anyway
				float directionLength = thisInput.magnitude;
				thisInput = thisInput / directionLength;

				// Make sure the length is no bigger than 1
				directionLength = Mathf.Min (1.0f, directionLength);

				// Make the input vector more sensitive towards the extremes and less sensitive in the middle
				// This makes it easier to control slow speeds when using analog sticks
				directionLength = directionLength * directionLength;

				// Multiply the normalized direction vector by the modified length
				thisInput = thisInput * directionLength;
				if (motor.isSprint)
					thisAnimator.SetInteger ("State", 2);
				else
					thisAnimator.SetInteger ("State", 1);
			} else {
				thisAnimator.SetInteger ("State", 0);
			}
		} else {
			thisAnimator.SetInteger ("State", 3);
		}
		// Apply the direction to the CharacterMotor
		motor.inputMoveDirection = transform.rotation * thisInput;
		motor.inputJump = player.GetButton ("Jump");
	}
}