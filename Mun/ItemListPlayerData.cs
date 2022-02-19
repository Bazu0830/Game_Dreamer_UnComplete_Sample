using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ItemListPlayerData : MonoBehaviour
{
    [SerializeField]
    AllDictionaryData alldata;
    [SerializeField]
    private GameObject iapitembutton = null;

    //　アイテムボタン一覧
    private List<GameObject> iapitembuttonList = new List<GameObject>();
    [SerializeField]
    GameObject content;
    [SerializeField]
    public PlayerData playerdata;


    //　キャラクターが持っているアイテムのボタン表示
    public void CreateItemButton()
    {
        int iapitembuttonNum = 0;
        GameObject itemButtonIns;
        //　選択したキャラクターのアイテム数分アイテムパネルボタンを作成
        //　持っているアイテム分のボタンの作成とクリック時の実行メソッドの設定
        foreach (var item in playerdata.itemDictionary.Keys)
        {
            Item a = alldata.itemdictionary.FirstOrDefault(d => d.Value == item).Key;
            itemButtonIns = content.transform.GetChild(iapitembuttonNum).gameObject;
            itemButtonIns.SetActive(true);
            itemButtonIns.transform.Find("ItemName").GetComponent<Text>().text = a.GetKanjiName();
            itemButtonIns.transform.Find("ItemImage").GetComponent<Image>().sprite = a.GetSprite();
            itemButtonIns.GetComponent<ItemPanelButtonPlayerData>().SetParam(a);
            itemButtonIns.GetComponent<ItemPanelButtonPlayerData>().playerdata = playerdata;
            itemButtonIns.GetComponent<ItemEquipPlayerData>().SetParam(a);
            itemButtonIns.GetComponent<ItemEquipPlayerData>().playerdata = playerdata;



            //　アイテムパネルボタン番号を更新
            iapitembuttonNum++;
        }
        for (int i = iapitembuttonNum; i < content.transform.childCount; i++)
        {
            content.transform.GetChild(i).gameObject.SetActive(false);

        }

    }
}
