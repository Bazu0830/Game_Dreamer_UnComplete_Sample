using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetPoint : MonoBehaviour
{
    [SerializeField]
    Item item;
    [SerializeField]
    int num;
    private AllyStatus allystatus;
   
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Mine"))
        {
            allystatus = other.GetComponent<UnityChanScript>().ally;

            if (allystatus.itemDictionary.ContainsKey(item))
            {
                allystatus.itemDictionary[item] += num;
            }
            else
            {
                allystatus.itemDictionary[item] = num;
            }

            Destroy(this.gameObject);
            Debug.Log("アイテムを拾った");
        }
    }
    
}
