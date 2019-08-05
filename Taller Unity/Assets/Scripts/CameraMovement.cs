using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Transform of the object to look at and the camera
    public Transform lookAt;
    public Transform camTransform;

    // Camera and vector of the distance between camera and object
    private Camera cam;
    private Vector3 distance;
    
    private void Awake()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Start()
    {
        camTransform = transform;
        cam = Camera.main;
        distance = new Vector3(0.0f, 5.0f, -10.0f);
    }
    
    void LateUpdate()
    {
        camTransform.position = lookAt.position + lookAt.rotation * distance;
        camTransform.LookAt(lookAt.position);
    }
}
