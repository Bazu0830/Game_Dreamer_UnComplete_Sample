using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlopeCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mine")
        {
            if (other.gameObject.GetComponent<UnityChanScript>().isSlope == false)
            {
                other.gameObject.GetComponent<UnityChanScript>().isSlope = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Mine")
        {
            other.gameObject.GetComponent<UnityChanScript>().isSlope = false;
        }
    }
}
