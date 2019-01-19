using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGoAway : MonoBehaviour
{
    [SerializeField] private int speed = 5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, 1 * Time.deltaTime * speed);
    }
}
