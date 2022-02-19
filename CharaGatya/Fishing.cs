using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fishing : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mine")
        {
            other.GetComponent<UnityChanScript>().SetWait();
            other.GetComponent<UnityChanScript>().FishingOpen();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mine")
        {
            other.GetComponent<UnityChanScript>().FishingClose();
        }
    }
}
