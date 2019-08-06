using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarDriving : MonoBehaviour
{
    // Speed for movement
    public float movementSpeed = 20.0f;
    public float rotationSpeed = 0.75f;

    // GameObject's needed
    public Collider coll;
    public Rigidbody rb;
    public Animator anim;

    float horizontalMove = 0.0f;
    Quaternion rotatePos;
    Vector3 movePos;
    public float maxRun = 2.0f;
    private float run = 0.00f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        anim.SetFloat("Moving",Input.GetAxis("Horizontal"));
    }

    void FixedUpdate()
    {
        Acc();
        // Movement and rotation (Normal)	
        if (Input.GetButton("Accelerate"))
        {
            run += 0.01f;
        }
        else if (Input.GetButton("Brake"))
        {
            run = 0.0f;
        }
        else
        {
            run -= 0.1f;
        }
        run = Mathf.Clamp(run, 0.0f, maxRun);
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
    //Collsions
    public IEnumerator Dash()
    {
        maxRun = 3.0f;
        run = maxRun;
        yield return new WaitForSeconds(3.0f);
        maxRun = 2.0f;
    }
}
