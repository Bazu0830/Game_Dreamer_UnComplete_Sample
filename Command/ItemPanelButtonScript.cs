using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemPanelButtonScript : MonoBehaviour, ISelectHandler
{

    private Item item;
    //　アイテムタイトル表示テキスト
    [SerializeField]
    Text itemname;
    //　アイテム情報表示テキスト
    [SerializeField]
    Text iteminformation;
    [SerializeField]
    Text itemnum;
    [SerializeField]
    GameObject itembuttonpanel;
    float time = 0f;
    public float life_time = 1.0f;
    public AllyStatus allystatus;
    public GameObject player;

    void Start()
    {
        ButtonOff();
    }

    //　ボタンが選択された時に実行
    public void OnSelect(BaseEventData eventData)
    {
        ShowItemInformation();
    }

    //　アイテム情報の表示
    public void ShowItemInformation()
    {
        itemname.text = item.GetKanjiName();
        iteminformation.text = item.GetInformation();
 //       itemnum.text = allystatus.GetItemNum(item).ToString();
    }
    //　データをセットする
    public void SetParam(Item item)
    {
        this.item = item;
    }
    public void ButtonOff()
    {
        itembuttonpanel.SetActive(false);
    }
    public void SetTime0()
    {
        time = 0;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (time > life_time)
        {
            itembuttonpanel.SetActive(false);
        }
    }
    public void PressItemButton()
    {
        if ((allystatus.itemDictionary[item] != 0))
        {
            itembuttonpanel.SetActive(true);
        }
        else
        {
            itembuttonpanel.SetActive(false);
        }
        
    }
}