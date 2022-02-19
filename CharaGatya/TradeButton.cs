using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TradeButton : MonoBehaviour
{
    [SerializeField]
    AllDictionaryData alldata;
    [SerializeField]
    PlayerData player;
    [SerializeField]
    int needMedal;
    [SerializeField]
    Item item=null;
    [SerializeField]
    AllyStatus ally =null;

    public void Trade()
    {
        if (player.medal >= needMedal)
        {
            player.medal -= needMedal;
            if (item != null)
            {
                int a = alldata.itemdictionary.FirstOrDefault(d => d.Key == item).Value;
                if (player.itemDictionary.ContainsKey(a))
                {
                    player.itemDictionary[a]  += 1;
                }
                else
                {
                    player.itemDictionary.Add(a, 1);
                }
            }
            else if (ally != null)
            {
                int a = alldata.allydictionary.FirstOrDefault(d => d.Key == ally).Value;
                if (player.allydictionary.ContainsKey(a))
                {
                    player.allydictionary[a] += 1;
                }
                else
                {
                    player.allydictionary.Add(a, 1);
                }
            }
            else
            {
                player.medal += needMedal;
            }
        }
    }
}
