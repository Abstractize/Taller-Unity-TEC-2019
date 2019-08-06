using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] cars = new GameObject[4];
    public GameObject cam;
    public int Players;

    // Start is called before the first frame update
    void Start()
    {

        for(int i = 0; i<4;i++)
        {
            GameObject Car = Instantiate(cars[i], this.transform.position + new Vector3(4*i,0,-10-4*i), Quaternion.identity);
            if (i < Players)
            {
                GameObject Cam = Instantiate(cam, Car.transform.position, this.transform.rotation);
                Cam.GetComponent<CameraMovement>().lookAt = Car.transform;
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
