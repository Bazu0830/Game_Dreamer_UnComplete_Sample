using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUseAndEquip : MonoBehaviour
{
    [SerializeField]
    GameObject itembutton;
    public AllyStatus allyStatus;
    [SerializeField]
    GameObject iapitembutton;
    public GameObject player;

    
    private Item item;
    //　データをセットする
    public void SetParam(Item item)
    {
        this.item = item;
    }

    public void UseItemToCharacter()
    {
       
        if (item.GetItemType() == Item.Type.HPRecovery)
        {
            if (allyStatus.hp >= allyStatus.maxHp)
            {
                Debug.Log("HPは満タンです");
            }
            else
            {
                allyStatus.hp += item.GetAmount();
                //　アイテムを使用した旨を表示
                //　持っているアイテム数を減らす
                allyStatus.itemDictionary[item] -= 1;
            }
        }
        else if (item.GetItemType() == Item.Type.MPRecovery)
        {
            if (allyStatus.mp >= allyStatus.maxMp)
            {
                Debug.Log("MPは満タンです");
            }
            else
            {
                allyStatus.mp += item.GetAmount();
                //　アイテムを使用した旨を表示
                //　持っているアイテム数を減らす
                allyStatus.itemDictionary[item] -= 1;
            }
        }
        else if (item.GetItemType() == Item.Type.PoisonRecovery)
        {
            if (!allyStatus.isPoisonState)
            {
                Debug.Log("毒状態ではありません");
            }
            else
            {
                allyStatus.isPoisonState = false;
                //　持っているアイテム数を減らす
                allyStatus.itemDictionary[item] -= 1;
            }
        }
        else if (item.GetItemType() == Item.Type.NumbnessRecovery)
        {
            if (!allyStatus.isNumbnessState)
            {
                Debug.Log("麻痺状態ではありません");
            }
            else
            {
                allyStatus.isNumbnessState =false;
                //　持っているアイテム数を減らす
                allyStatus.itemDictionary[item] -= 1;
            }
        }


        //　アイテム数が0だったらボタンとキャラクターステータスからアイテムを削除
        if (allyStatus.itemDictionary[item] == 0)
        {
            //　アイテムを渡したキャラクター自身のItemDictionaryからそのアイテムを削除
            allyStatus.itemDictionary.Remove(item);
            Destroy(iapitembutton);
        }

    }
}
