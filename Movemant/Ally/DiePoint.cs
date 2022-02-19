using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiePoint : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mine")
        {
            other.GetComponent<UnityChanScript>().ally.hp=0;
        }
    }
}
