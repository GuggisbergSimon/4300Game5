using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	//[SerializeField] private GameObject target = null;
	[SerializeField, Range(0.1f, 100.0f)] private float distanceDollyStart = 10.0f;
	[SerializeField, Range(0.1f, 179.0f)] private float limitAngleFOV = 90.0f;
	[SerializeField, Range(0.1f, 90.0f)] private float limitAngleTilt = 45.0f;
	[SerializeField] private float speedZoom = 7.0f;
	[SerializeField] private float speedTilt = 7.0f;
	private Camera myCamera;
	private float initFrustumHeight;
	private float initFOV;
	private Vector3 initRot;
	private Vector3 initPos;

	private float FrustumHeightForDistanceAndFOV(float distance, float FOV)
	{
		return 2.0f * distance * Mathf.Tan(FOV * Mathf.Deg2Rad / 2.0f);
	}

	// Calculate the FOV needed to get a given frustum height at a given distance.
	private float FOVForHeightAndDistance(float height, float distance)
	{
		return 2.0f * Mathf.Atan(height / (2.0f * distance)) * Mathf.Rad2Deg;
	}

	private float DistanceForHeightAndFOV(float height, float FOV)
	{
		return height / (2.0f * Mathf.Tan(FOV * Mathf.Deg2Rad / 2.0f));
	}

	private void Start()
	{
		myCamera = GetComponent<Camera>();
		initFrustumHeight = FrustumHeightForDistanceAndFOV(distanceDollyStart, myCamera.fieldOfView);
		initFOV = myCamera.fieldOfView;
		initPos = transform.localPosition;
		initRot = transform.localRotation.eulerAngles;
	}

	private void Update()
	{
		ApplyFOV();
		ApplyRotation();
	}

	void ApplyFOV()
	{
		float FOV = initFOV + Mathf.Pow(transform.position.x, 2.0f) * speedZoom;
		if (FOV < limitAngleFOV)
		{
			myCamera.fieldOfView = FOV;
			transform.localPosition = Vector3.right * initPos.x + Vector3.up * initPos.y + Vector3.forward *
			                          (distanceDollyStart -
			                           DistanceForHeightAndFOV(initFrustumHeight, myCamera.fieldOfView));
		}
	}

	private void ApplyRotation()
	{
		float tilt = transform.position.x * speedTilt;
		if (tilt < limitAngleTilt)
		{
			transform.localRotation = Quaternion.Euler(initRot - Vector3.forward * tilt);
		}
	}
}