using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenTheDoor : MonoBehaviour
{
    [SerializeField]
    Item item;
    [SerializeField]
    GameObject door;
    private AllyStatus allystatus;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            allystatus = other.GetComponent<UnityChanScript>().ally;
            if (allystatus.itemDictionary.ContainsKey(item))
            {
                door.GetComponent<BoxCollider>().enabled = false;
            }
        }
        
    }
}
