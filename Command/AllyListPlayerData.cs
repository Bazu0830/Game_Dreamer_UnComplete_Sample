using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AllyListPlayerData : MonoBehaviour
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
    public void CreateAllyButton()
    {
        int iapitembuttonNum = 0;
        GameObject itemButtonIns;
        //　選択したキャラクターのアイテム数分アイテムパネルボタンを作成
        //　持っているアイテム分のボタンの作成とクリック時の実行メソッドの設定
        foreach (var ally in playerdata.allydictionary.Keys)
        {
            AllyStatus a = alldata.allydictionary.FirstOrDefault(d => d.Value == ally).Key;
            itemButtonIns = content.transform.GetChild(iapitembuttonNum).gameObject;
            itemButtonIns.SetActive(true);
    //        itemButtonIns.transform.Find("ItemName").GetComponent<Text>().text = a.GetCharacterName();
            itemButtonIns.GetComponent<AllyPanelButtonPlayerData>().SetParam(a);
            itemButtonIns.GetComponent<AllyPanelButtonPlayerData>().playerdata = playerdata;
            itemButtonIns.GetComponent<AllyEquipPlayerData>().SetParam(a);
            itemButtonIns.GetComponent<AllyEquipPlayerData>().playerdata = playerdata;



            //　アイテムパネルボタン番号を更新
            iapitembuttonNum++;
        }
        for (int i = iapitembuttonNum; i < content.transform.childCount; i++)
        {
            content.transform.GetChild(i).gameObject.SetActive(false);

        }

    }
}