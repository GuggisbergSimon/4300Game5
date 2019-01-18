using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	[SerializeField] private GameObject target = null;
	[SerializeField] private float speedZoom = 7.0f;
	[SerializeField] private float speedTilt = 7.0f;
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
		//todo remove the two following lines, only for test purposes
		myCamera.fieldOfView += Input.GetAxis("Vertical") * Time.deltaTime * speedZoom;
		Quaternion targetRot = Quaternion.Euler(0f, 0f, -Input.GetAxisRaw("Horizontal")*90);
		
		//Handle the rotation of the camera
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, speedTilt * Time.deltaTime);
		//handle the position on the camera, based on fov
		transform.position = initPos.x * Vector3.right + initPos.y * Vector3.up +
		                     Vector3.forward * target.transform.position.z - Vector3.forward *
		                     DistanceForHeightAndFOV(initHeight, myCamera.fieldOfView);
	}
}