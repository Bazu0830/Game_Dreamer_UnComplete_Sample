using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollider : MonoBehaviour
{
    [SerializeField][TextArea(1, 100)]
    private string talkall;
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mine")
        {
            other.GetComponent<UnityChanScript>().SetWait();
            other.GetComponent<UnityChanScript>().npctalking = talkall;
            other.GetComponent<UnityChanScript>().NpcTalk();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mine")
        {
            other.GetComponent<UnityChanScript>().NotNpcTalk();
        }
    }
}
