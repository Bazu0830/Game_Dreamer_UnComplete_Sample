using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnimEvent : MonoBehaviour
{
    [SerializeField]
    private UnityChanScript ally;
    [SerializeField]
    private Transform equipleft;
    [SerializeField]
    private Transform equipright;
   
    void LAttackStart()
    {
        equipleft.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    void LAttackEnd()
    {
        equipleft.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    void RAttackStart()
    {
        equipright.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = true;
    }
    void RAttackEnd()
    {
        equipright.GetChild(0).gameObject.GetComponent<BoxCollider>().enabled = false;
    }
}
