using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speedForward = 5.0f;
	[SerializeField] private float speedLateral = 5.0f;
	private const float MIN_SPEED_THRESHOLD = 0.1f;
	private GameObject ground;
	private bool canMove = true;
	private bool isAlive = true;
	private Rigidbody myRigidbody;

	private void Start()
	{
		myRigidbody = GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (canMove)
		{
			transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speedLateral, 0,
				1 * Time.deltaTime * speedForward);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Ground")
		{
			canMove = true;
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.tag == "Ground")
		{
			canMove = false;
		}
	}

	public void Die()
	{
		if (isAlive)
		{
			isAlive = false;
			canMove = false;
			myRigidbody.velocity = Vector3.zero;
			Destroy(myRigidbody);
		}
	}

	public IEnumerator StopMoving(float time)
	{
		while (speedForward > MIN_SPEED_THRESHOLD)
		{
			speedForward = Mathf.Lerp(speedForward, 0.0f, Time.deltaTime);
			yield return null;
		}

		speedForward = 0.0f;
	}
}