using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speedForward = 5.0f;
	[SerializeField] private float speedLateral = 5.0f;
	[SerializeField] private float timeStopMoving = 2.0f;
	private GameObject ground;
	private bool isAlive = true;
	private bool canMove = true;
	public bool CanMove => canMove;

	private Rigidbody myRigidBody;
	public Rigidbody MyRigidBody => myRigidBody;

	private void Start()
	{
		myRigidBody = GetComponent<Rigidbody>();
	}

	

	void Update()
	{
		if (canMove)
		{
			transform.Translate(Input.GetAxis("Horizontal") * Time.deltaTime * speedLateral, 0, Time.deltaTime * speedForward);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.CompareTag("Ground"))
		{
			canMove = true;
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (other.gameObject.CompareTag("Ground"))
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
			myRigidBody.velocity = Vector3.zero;
			Destroy(myRigidBody);
		}
	}

	public IEnumerator StopMoving()
	{
		float timer = 0.0f;
		while (timer < timeStopMoving)
		{
			timer += Time.deltaTime;
			speedForward = Mathf.Lerp(speedForward, 0.0f, timer / timeStopMoving);
			yield return null;
		}

		speedForward = 0.0f;
	}
}