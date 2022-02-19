using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemListScript1 : MonoBehaviour
{
    [SerializeField]
    private GameObject iapitembutton = null;

    //　アイテムボタン一覧
    private List<GameObject> iapitembuttonList = new List<GameObject>();
    [SerializeField]
    GameObject content;
    
    public GameObject player;
    
    public AllyStatus allyStatus;

    
    //　キャラクターが持っているアイテムのボタン表示
    public void CreateItemButton()
    {
        int iapitembuttonNum = 0;
        GameObject itemButtonIns;
        //　選択したキャラクターのアイテム数分アイテムパネルボタンを作成
        //　持っているアイテム分のボタンの作成とクリック時の実行メソッドの設定
        foreach (var item in allyStatus.itemDictionary.Keys)
        {
            itemButtonIns = content.transform.GetChild(iapitembuttonNum).gameObject;
            itemButtonIns.SetActive(true);
            itemButtonIns.transform.Find("ItemName").GetComponent<Text>().text = item.GetKanjiName();
            itemButtonIns.transform.Find("ItemImage").GetComponent<Image>().sprite = item.GetSprite();
            itemButtonIns.GetComponent<ItemPanelButtonScript>().SetParam(item);
            itemButtonIns.GetComponent<ItemPanelButtonScript>().allystatus = allyStatus;
            itemButtonIns.GetComponent<ItemPanelButtonScript>().player = player;
            itemButtonIns.GetComponent<ItemUseAndEquip>().SetParam(item);
            itemButtonIns.GetComponent<ItemUseAndEquip>().allyStatus = allyStatus;
            itemButtonIns.GetComponent<ItemUseAndEquip>().player = player;



            //　アイテムパネルボタン番号を更新
            iapitembuttonNum++;
        }
        for (int i = iapitembuttonNum; i < content.transform.childCount; i++)
        {
            content.transform.GetChild(i).gameObject.SetActive(false);

        }

    }
}
