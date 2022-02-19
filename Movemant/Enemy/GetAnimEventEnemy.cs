using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnimEventEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform Lequip;
    [SerializeField]
    private Transform Requip;
    [SerializeField]
    private EnemyAI enemyAI;

    void LAttackStart()
    {
        Lequip.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
    }

    void LAttackEnd()
    {
        Lequip.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
    }
    void RAttackStart()
    {
        Requip.transform.GetChild(0).GetComponent<BoxCollider>().enabled = true;
    }

    void RAttackEnd()
    {
        Requip.transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
    }
}
