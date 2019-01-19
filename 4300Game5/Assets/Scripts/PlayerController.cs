using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speedForward = 5.0f;
    [SerializeField] private float speedLateral = 5.0f;

    private GameObject ground;

    private bool canMove = true;

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime*speedLateral,0,1*Time.deltaTime*speedForward);
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

    public IEnumerator StopMoving(float time)
    {
        //speedForward=
        yield return null;
    }
}
