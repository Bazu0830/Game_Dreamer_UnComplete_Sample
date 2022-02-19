using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MonobitEngine;

public class SaveDataScript : MonobitEngine.MonoBehaviour
{
    
    public AllyStatus unityChanStatus = null;
   
    public Transform unityChanTransform;
    [SerializeField]
    PositionData positiondata;
    //　Saveメソッド
    public void Save(string Dataname)
    {
        Data data = new Data();
        data.Position = positiondata.GetPosition();
        //　ユニティちゃんデータをセット
        data.UnityChanCharacterName = unityChanStatus.characterName;
        data.UnityChanMaxHP = unityChanStatus.maxHp;
        data.UnityChanMaxMP = unityChanStatus.maxMp;
        data.UnityChanHP = unityChanStatus.hp;
        data.UnityChanMP = unityChanStatus.mp;
        data.UnityChanIsNumbnessState = unityChanStatus.isNumbnessState;
        data.UnityChanIsPoisonState = unityChanStatus.isPoisonState;

        var unityChanItemDictionaryKeys = new List<Item>();
        var unityChanItemDictionaryValues = new List<int>();

        foreach (var item in unityChanStatus.itemDictionary)
        {
            unityChanItemDictionaryKeys.Add(item.Key);
            unityChanItemDictionaryValues.Add(item.Value);
        }
        data.UnityChanItemDictionaryKeys = unityChanItemDictionaryKeys;
        data.UnityChanItemDictionaryValues = unityChanItemDictionaryValues;
       // data.UnityChanSkillDictionaryKeys = unityChanStatus.skillList;
        data.UnityChanPA = unityChanStatus.PA;
        data.UnityChanMA = unityChanStatus.MA;
        data.UnityChanPD = unityChanStatus.PD;
        data.UnityChanMD = unityChanStatus.MD;
        data.UnityChanMoney = unityChanStatus.money;

        PlayerPrefs.SetString(Dataname + MonobitNetwork.room.parametersListedInLobby[2], JsonUtility.ToJson(data));
        PlayerPrefs.Save();
    }
}
