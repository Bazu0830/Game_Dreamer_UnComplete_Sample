using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCollider : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mine")
        {
            if (other.gameObject.GetComponent<UnityChanScript>().isLadder == false)
            {
                other.gameObject.GetComponent<UnityChanScript>().isLadder = true;
                other.gameObject.GetComponent<UnityChanScript>().laddervec = transform;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Mine")
        {
            other.gameObject.GetComponent<UnityChanScript>().isLadder = false;
        }
    }
}
