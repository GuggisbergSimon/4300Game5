using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
	[SerializeField] private float speedForward = 5.0f;
	[SerializeField] private float speedLateral = 5.0f;
	[SerializeField] private float timeStopMoving = 2.0f;
	[SerializeField] private float timeFadingToBlack = 3.0f;
	[SerializeField] private float minForceRandom = 0.1f;
	[SerializeField] private float maxForceRandom = 2.0f;
	[SerializeField] private AudioClip[] stepSounds = null;
	[SerializeField] private AudioClip fallingSound = null;
	[SerializeField] private AudioClip[] splatSounds = null;
	private GameObject ground;
	private float horizontalInput = 0.0f;
	private bool isAlive = true;
	private bool canMove = false;
	private bool canInput = true;
	private AudioSource myAudioSource;
	private TriggerDetector myTriggerDetector;
	private float timeOutOfMenu = 0.0f;
	private List<Collision> isTouching = new List<Collision>();

	public bool CanMove
	{
		get => canMove;
		set => canMove = value;
	}

	private Rigidbody myRigidBody;

	private void Start()
	{
		myTriggerDetector = GetComponentInChildren<TriggerDetector>();
		myRigidBody = GetComponent<Rigidbody>();
		myAudioSource = GetComponent<AudioSource>();
		if (minForceRandom > maxForceRandom)
		{
			minForceRandom = maxForceRandom;
		}
		else if (maxForceRandom < minForceRandom)
		{
			maxForceRandom = minForceRandom;
		}
	}

	private void FixedUpdate()
	{
		if (canMove)
		{
			if (!myTriggerDetector.isTouchingAnything())
			{
				Falling();
				return;
			}
			myRigidBody.velocity = new Vector3(speedLateral * horizontalInput, myRigidBody.velocity.y,
				speedForward * Mathf.Sign(transform.forward.z));
			timeOutOfMenu += Time.deltaTime;
			Destabilize();
		}
	}

	void Update()
	{
		if (canInput)
		{
			horizontalInput = Input.GetAxis("Horizontal");
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		if (!isTouching.Contains(other))
		{
			isTouching.Add(other);
		}
	}

	private void OnCollisionExit(Collision other)
	{
		if (isTouching.Contains(other))
		{
			isTouching.Remove(other);
			if (isTouching.Count == 0 && other.gameObject.CompareTag("Ground") &&
			    Mathf.Abs(myRigidBody.velocity.y) > 0.001f)
			{
				Falling();
			}
		}
	}

	private void Falling()
	{
		canMove = false;
		StartCoroutine(GameManager.Instance.FadeToBlack(timeFadingToBlack));
		myAudioSource.clip = fallingSound;
		myAudioSource.loop = true;
		myAudioSource.Play();
	}

	private IEnumerator PlayStepSound()
	{
		while (canMove)
		{
			myAudioSource.PlayOneShot(stepSounds[Random.Range(0, stepSounds.Length)]);
			if (myRigidBody.velocity.z.CompareTo(0) != 0)
			{
				yield return new WaitForSeconds(Mathf.Sqrt(2) / myRigidBody.velocity.z);
			}
			else
			{
				yield return null;
			}
		}
	}

	private void Destabilize()
	{
		float pos = transform.position.x;
		if (pos.CompareTo(0) == 0)
		{
			pos *= Mathf.Pow(-1.0f, Random.Range(0, 1));
		}

		myRigidBody.AddForce(Vector3.right * timeOutOfMenu * Mathf.Sign(pos) *
		                     Random.Range(minForceRandom, maxForceRandom));
	}

	public void Die()
	{
		if (isAlive)
		{
			isAlive = false;
			canMove = false;
			myRigidBody.velocity = Vector3.zero;
			Destroy(myRigidBody);
			myAudioSource.Stop();
			myAudioSource.PlayOneShot(splatSounds[Random.Range(0, splatSounds.Length)]);
		}
	}

	public IEnumerator StopMoving()
	{
		minForceRandom = 0.0f;
		maxForceRandom = 0.0f;
		canInput = false;
		horizontalInput = 0.0f;
		myRigidBody.velocity = Vector3.zero;
		float initSpeed = speedForward;
		Vector3 initPos = transform.position;
		float timer = 0.0f;
		while (timer < timeStopMoving)
		{
			timer += Time.deltaTime;
			transform.position = Vector3.up * transform.position.y + Vector3.forward * transform.position.z +
			                     Vector3.right * Mathf.Lerp(initPos.x, 0.0f, timer / timeStopMoving);
			speedForward = Mathf.Lerp(initSpeed, 0.0f, timer / timeStopMoving);
			yield return null;
		}

		canMove = false;
		speedForward = 0.0f;
	}
}