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

    // Flags for drifting
    private bool driftDir = true;       // True = left || False = right
    private bool drift = false;

    private void Start()
    {
        coll = GetComponentInChildren<Collider>();
    }

    private void Update()
    {
        // Drifting Flag
        if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.A) && !drift && IsGrounded())
        {
            drift = true;
            driftDir = true;
            car.transform.Rotate(0.0f, -40.0f, 0.0f);
            car.GetComponent<Rigidbody>().velocity += new Vector3(0.0f, 3.0f, 0.0f);
        } else if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.D) && IsGrounded())
        {
            drift = true;
            driftDir = false;
            car.transform.Rotate(0.0f, 40.0f, 0.0f);
            car.GetComponent<Rigidbody>().velocity += new Vector3(0.0f, 3.0f, 0.0f);
        }
        if (Input.GetKeyUp(KeyCode.Space) && drift)
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
        // Movement
        if (!drift || (drift && !IsGrounded()))
        {
            // Movement and rotation (Normal)
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(-transform.up * rotationSpeed);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(transform.up * rotationSpeed);
                }
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(-Vector3.forward * movementSpeed * Time.deltaTime);

                if (Input.GetKey(KeyCode.A))
                {
                    transform.Rotate(transform.up * rotationSpeed);
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    transform.Rotate(-transform.up * rotationSpeed);
                }
            }
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
    }
    
    public bool IsGrounded()
    {
        bool hit = Physics.Raycast(coll.bounds.center, Vector3.down, coll.bounds.extents.y + 0.1f);
        return hit;
    }
}
