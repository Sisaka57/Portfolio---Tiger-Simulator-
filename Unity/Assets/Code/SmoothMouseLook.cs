using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Rewired;

[AddComponentMenu("Camera-Control/Smooth Mouse Look")]
public class SmoothMouseLook : MonoBehaviour {

	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationX = 0F;
	float rotationY = 0F;

	public float frameCounter = 20;

	private int playerID = 0;
	private Player player;

	// Use this for initialization
	private void Awake ()
	{
		player = ReInput.players.GetPlayer (playerID);
		//Cursor.lockState = CursorLockMode.Locked;
	}

	Quaternion originalRotation;

	void Update ()
	{
		if (Input.GetKey (KeyCode.Escape))
			Cursor.lockState = CursorLockMode.None;
		if (axes == RotationAxes.MouseX)
		{	
			rotationX += player.GetAxis ("Look Horizontal") * sensitivityX;

			rotationX = ClampAngle (rotationX, minimumX, maximumX);

			Quaternion xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			transform.localRotation = originalRotation * xQuaternion;			
		}
		else
		{
			rotationY += player.GetAxis ("Look Vertical") * sensitivityY;

			rotationY = ClampAngle (rotationY, minimumY, maximumY);

			Quaternion yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);
			transform.localRotation = originalRotation * yQuaternion;
		}
	}

	void Start ()
	{		
		Rigidbody rb = GetComponent<Rigidbody>();	
		if (rb)
			rb.freezeRotation = true;
		originalRotation = transform.localRotation;
	}

	public static float ClampAngle (float angle, float min, float max)
	{
		angle = angle % 360;
		if ((angle >= -360F) && (angle <= 360F)) {
			if (angle < -360F) {
				angle += 360F;
			}
			if (angle > 360F) {
				angle -= 360F;
			}			
		}
		return Mathf.Clamp (angle, min, max);
	}
}