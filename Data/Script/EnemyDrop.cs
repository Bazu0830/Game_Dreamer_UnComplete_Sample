using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDrop : MonoBehaviour
{

    [SerializeField]
    Item item;
    [SerializeField]
    int num;
    private AllyStatus allystatus;
    [SerializeField]
    int money;
   

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            allystatus = other.GetComponent<UnityChanScript>().ally;

            if (allystatus.itemDictionary.ContainsKey(item))
            {
                allystatus.itemDictionary[item] += num;
            }
            else
            {
                allystatus.itemDictionary[item]= num;
            }
            allystatus.money+=money;

            Destroy(this.gameObject);
            Debug.Log("アイテムを拾った");
        }
    }

}
