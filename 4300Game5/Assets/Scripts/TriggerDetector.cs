using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetector : MonoBehaviour
{
	private int numberColliding = 0;
	
	private void OnTriggerEnter(Collider other)
	{
		numberColliding++;
	}

	private void OnTriggerExit(Collider other)
	{
		numberColliding--;
	}

	public bool isTouchingAnything()
	{
		return numberColliding > 0;
	}
}
