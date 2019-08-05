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

    // Flags for drifting
    private bool driftDir = true;       // True = left || False = right
    private bool drift = false;

    float horizontalMove = 0.0f;
    public Camera cam;
    private Vector3 camForward;
    private Vector3 camRight;
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
        // Drifting Flag
        if (Input.GetButtonDown("Drift") && IsMoving() && !drift && IsGrounded())
        {
            drift = true;
            driftDir = true;
            car.transform.Rotate(0.0f, Input.GetAxis("Horizontal") * 40.0f, 0.0f);
            car.GetComponent<Rigidbody>().velocity += new Vector3(0.0f, 3.0f, 0.0f);
        } else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D) && IsGrounded())
        if (Input.GetButtonUp("Drift") && drift)
        {
            drift = false;
            if (driftDir)
            {
                car.transform.Rotate(0.0f, 40.0f, 0.0f);
            } else
            {
                car.transform.Rotate(0.0f, -40.0f, 0.0f);
            }
        }
    }

    void FixedUpdate()
    {
        Acc();
        
        
        // Movement
        if (!drift || (drift && !IsGrounded()))
        {
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
            run = Mathf.Clamp(run, 0.0f, 2.0f);
        }
        // Movement and rotation (Drifting)
        else if (drift && IsGrounded())
        {
            transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime / 1.75f);
            if (driftDir)
            {
                transform.Rotate(-transform.up * rotationSpeed * 2f);
            } else
            {
                transform.Rotate(transform.up * rotationSpeed * 2f);
            }
        }
        rb.MovePosition(transform.position + movePos * Time.deltaTime);
        rb.MoveRotation(transform.rotation * rotatePos);
    }
    void Acc()
    {
        CamDirection();
        horizontalMove = Input.GetAxis("Horizontal");
        rotatePos = Quaternion.Euler(0.0f, horizontalMove * rotationSpeed, 0.0f);
        movePos = transform.forward * movementSpeed * run;
        
    }
    protected void CamDirection()
    {
        camForward = cam.transform.forward;
        camRight = cam.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
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
