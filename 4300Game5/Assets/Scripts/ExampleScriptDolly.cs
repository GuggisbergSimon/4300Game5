using UnityEngine;
using System.Collections;

public class ExampleScriptDolly : MonoBehaviour
{
	[SerializeField] private GameObject target;
	private Camera myCamera;

	private float initHeightAtDist;
	private bool dzEnabled;

	// Calculate the frustum height at a given distance from the camera.
	private float FrustumHeightAtDistance(float distance)
	{
		return 2.0f * distance * Mathf.Tan(myCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
	}

	// Calculate the FOV needed to get a given frustum height at a given distance.
	private float FOVForHeightAndDistance(float height, float distance)
	{
		return 2.0f * Mathf.Atan(height * 0.5f / distance) * Mathf.Rad2Deg;
	}

	// Start the dolly zoom effect.
	void StartDZ()
	{
		var distance = Vector3.Distance(transform.position, target.transform.position);
		initHeightAtDist = FrustumHeightAtDistance(Vector3.Distance(transform.position, target.transform.position));
		dzEnabled = true;
	}

	// Turn dolly zoom off.
	void StopDZ()
	{
		dzEnabled = false;
	}

	void Start()
	{
		myCamera = GetComponent<Camera>();
		StartDZ();
	}

	void Update()
	{
		if (dzEnabled)
		{
			// Measure the new distance and readjust the FOV accordingly.
			var currDistance = Vector3.Distance(transform.position, target.transform.position);
			myCamera.fieldOfView = FOVForHeightAndDistance(initHeightAtDist, Vector3.Distance(transform.position, target.transform.position));
		}

		// Simple control to allow the camera to be moved in and out using the up/down arrows.
		transform.Translate(Input.GetAxis("Vertical") * Vector3.forward * Time.deltaTime * 5f);
	}
}