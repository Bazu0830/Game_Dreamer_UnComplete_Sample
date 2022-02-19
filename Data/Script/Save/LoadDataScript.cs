using System;
using UnityEngine;
using MonobitEngine;

[Serializable]
public class LoadDataScript : MonobitEngine.MonoBehaviour
{

    
    public AllyStatus unityChanStatus = null;
    [SerializeField]
    public PositionData positiondata;
    //　データ読み込みメソッド
    public void Load(string dataName)
    {
        //     if (PlayerPrefs.HasKey(dataName + PhotonNetwork.room.customParameters["stage"]))
        {

            var data = JsonUtility.FromJson<Data>(PlayerPrefs.GetString(dataName + MonobitNetwork.room.parametersListedInLobby[2]));

            //　シーンデータのセット
            positiondata.SetPosition(data.Position);

            //　ユニティちゃんデータをセット
            unityChanStatus.characterName=data.UnityChanCharacterName;
            //  Max系のメソッドを先に実行する必要あり
            unityChanStatus.maxHp=data.UnityChanMaxHP;
            unityChanStatus.maxMp=data.UnityChanMaxMP;
            unityChanStatus.hp=data.UnityChanHP;
            unityChanStatus.mp=data.UnityChanMP;
            unityChanStatus.isNumbnessState=data.UnityChanIsNumbnessState;
            unityChanStatus.isPoisonState=data.UnityChanIsPoisonState;
          //  unityChanStatus.CreateItemDictionary(data.UnityChanItemDictionary);

            var unityChanItemDictionary = new ItemDictionary();
            for (int i = 0; i < data.UnityChanItemDictionaryKeys.Count; i++)
            {
          //      unityChanItemDictionary.Add(data.UnityChanItemDictionaryKeys[i], data.UnityChanItemDictionaryValues[i]);
            }
          //  unityChanStatus.CreateItemDictionary(unityChanItemDictionary);
          //  unityChanStatus.skillList=data.UnityChanSkillDictionaryKeys;

            unityChanStatus.PA=data.UnityChanPA;
            unityChanStatus.MA=data.UnityChanMA;
            unityChanStatus.PD=data.UnityChanPD;
            unityChanStatus.MD=data.UnityChanMD;
            unityChanStatus.money=data.UnityChanMoney;
        }
    }
}