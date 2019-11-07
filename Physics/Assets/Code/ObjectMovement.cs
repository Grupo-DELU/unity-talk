using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    public float speed = 10.00f;
    private Rigidbody rb;
    private Vector3 dir;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        dir = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
    }

    private void FixedUpdate()
    {
        //RbVelocityMovement(dir);
        //RbForceMovement(dir);
        RbMovePosition(dir);
    }

    void RbVelocityMovement(Vector3 direction)
    {
        rb.velocity = direction * speed;
    }

    void RbForceMovement(Vector3 direction)
    {
        rb.AddForce(direction * speed);
    }

    void RbMovePosition(Vector3 direction)
    {
        rb.MovePosition(transform.position + (direction * speed * Time.deltaTime));
    }
}
