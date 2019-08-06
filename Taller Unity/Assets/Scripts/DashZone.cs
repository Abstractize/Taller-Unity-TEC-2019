using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag.Equals("Player"))
            StartCoroutine(other.gameObject.GetComponent<CarDriving>().Dash());
    }
}
