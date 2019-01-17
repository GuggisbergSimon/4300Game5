using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField, Range(0, 180)] private float fieldOfView = 45.0f;
	[SerializeField] private float width = 1.0f;
	private Camera myCamera;
	private Vector3 originPosition;
	private float originFOV;

	private void Start()
	{
		myCamera = GetComponent<Camera>();
		originFOV = myCamera.fieldOfView;
		originPosition = transform.position;
	}

	private void Update()
	{
		myCamera.fieldOfView = fieldOfView;
		transform.position = originPosition + Vector3.forward * width/(2*Mathf.Tan(fieldOfView/2));

		//width that'll remains unchanged
		//distance the camera has to move
		//FOV fov
		//distance = width/2tan(FOV/2)
	}
}