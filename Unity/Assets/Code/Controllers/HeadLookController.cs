using UnityEngine;
using System.Collections;

public class HeadLookController : MonoBehaviour {

	[SerializeField]
	private Transform HeadBone;
	[SerializeField]
	private Transform Target;

	private float rotationY = 0f;

	private void LateUpdate()
	{
		Vector3 targetPosition = new Vector3 (Target.position.x, Target.position.y, Target.position.z);
		HeadBone.LookAt (targetPosition);
	}
}
