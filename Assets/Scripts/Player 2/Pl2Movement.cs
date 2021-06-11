using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pl2Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    private float thrust = 500;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        if (Input.GetKey("a"))
        {
            rb.AddRelativeForce(transform.right * -thrust);
        }
        if (Input.GetKey("d"))
        {
            rb.AddRelativeForce(transform.right * thrust);
        }
    }
}
