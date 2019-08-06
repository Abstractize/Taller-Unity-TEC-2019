using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour
{
    // Speed for movement
    public float movementSpeed = 20.0f;
    public float rotationSpeed = 0.75f;

    // GameObject's needed
    public GameObject car;
    public Collider coll;
    public Rigidbody rb;

    float horizontalMove = 0.0f;
    Quaternion rotatePos;
    Vector3 movePos;
    private float run = 0.00f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
    }

    private void Update()
    {

    }

    void FixedUpdate()
    {
        Acc();
        rb.MovePosition(transform.position + movePos * Time.deltaTime);
        rb.MoveRotation(transform.rotation * rotatePos);
    }
    void Acc()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        rotatePos = Quaternion.Euler(0.0f, horizontalMove * rotationSpeed, 0.0f);
        movePos = transform.forward * movementSpeed * run;
        
    }
    public bool IsMoving()
    {
        return !Input.GetAxis("Horizontal").Equals(0.0f);
    }
    public bool IsGrounded()
    {
        bool hit = Physics.Raycast(coll.bounds.center, Vector3.down, coll.bounds.extents.y + 0.1f);
        return hit;
    }
}
