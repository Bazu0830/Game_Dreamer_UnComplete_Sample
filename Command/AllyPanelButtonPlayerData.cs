using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AllyPanelButtonPlayerData : MonoBehaviour
{
    [SerializeField]
    AllDictionaryData alldata;
    private AllyStatus ally;
    //　アイテムタイトル表示テキスト
    [SerializeField]
    Text allyname;
    //　アイテム情報表示テキスト
    [SerializeField]
    Text allyinformation;
    [SerializeField]
    Text allynum;
    [SerializeField]
    GameObject allybuttonpanel;
    float time = 0f;
    public float life_time = 1.0f;
    [SerializeField]
    public PlayerData playerdata;

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
       // allyname.text = ally.GetCharacterName();
       // allyinformation.text = ally.GetInformation();
        int a = alldata.allydictionary.FirstOrDefault(d => d.Key == ally).Value;
        allynum.text = playerdata.allydictionary[a].ToString();
    }
    //　データをセットする
    public void SetParam(AllyStatus ally)
    {
        this.ally = ally;
    }
    public void ButtonOff()
    {
        allybuttonpanel.SetActive(false);
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
            allybuttonpanel.SetActive(false);
        }
    }
    public void PressItemButton()
    {
        int a = alldata.allydictionary.FirstOrDefault(d => d.Key == ally).Value;

        if ((playerdata.allydictionary[a] != 0))
        {
            allybuttonpanel.SetActive(true);
        }
        else
        {
            allybuttonpanel.SetActive(false);
        }

    }
}
