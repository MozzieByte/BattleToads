using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	private Vector3 Offset = new Vector3(0, 0, -10);
	private float Smooth_Time = 0.25f;
	private Vector3 Velocity = Vector3.zero;

	[SerializeField]
	private Transform Target;

    void Update()
    {
		Vector3 Target_Position = Target.position + Offset;
		transform.position = Vector3.SmoothDamp(transform.position, Target_Position, ref Velocity, Smooth_Time);
    }
}
