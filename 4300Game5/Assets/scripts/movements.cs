using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movements : MonoBehaviour
{
    [SerializeField] private int speed = 5;

    private GameObject ground;

    private bool canMove = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime*speed,0,1*Time.deltaTime*speed);
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
}
