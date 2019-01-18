using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private GameObject target;
	[SerializeField] private float speedZoom = 7.0f;
	private Camera myCamera;
	private float initHeight;
	private Vector3 initPos;

	private float FrustumHeightForDistanceAndFOV(float distance, float FoV)
	{
		return 2.0f * distance * Mathf.Tan(FoV * Mathf.Deg2Rad / 2.0f);
	}

	// Calculate the FOV needed to get a given frustum height at a given distance.
	private float FOVForHeightAndDistance(float height, float distance)
	{
		return 2.0f * Mathf.Atan(height / (2.0f * distance)) * Mathf.Rad2Deg;
	}

	private float DistanceForHeightAndFOV(float height, float FoV)
	{
		return height / (2.0f * Mathf.Tan(FoV * Mathf.Deg2Rad / 2.0f));
	}

	private void Start()
	{
		myCamera = GetComponent<Camera>();
		float distance = Vector3.Distance(transform.position, target.transform.position);
		initHeight = FrustumHeightForDistanceAndFOV(distance, myCamera.fieldOfView);
		initPos = transform.position;
	}

	private void Update()
	{
		//todo remove the following line, only for test purposes
		myCamera.fieldOfView += Input.GetAxis("Vertical") * Time.deltaTime * speedZoom;
		transform.position = initPos.x * Vector3.right + initPos.y * Vector3.up +
		                     Vector3.forward * target.transform.position.z - Vector3.forward *
		                     DistanceForHeightAndFOV(initHeight, myCamera.fieldOfView);
	}
}