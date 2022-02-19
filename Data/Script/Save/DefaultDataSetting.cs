using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DefaultDataSetting : MonoBehaviour
{
    [SerializeField] public PlayerData playerdata;
    public AllyStatus unityChanStatus = null;

    public void Load()
    {
        //  Max系のメソッドを先に実行する必要あり
       // if (unityChanStatus.itemDictionary.ContainsKey(playerdata.equipWeapon))
       // {
           // unityChanStatus.SetItemNum(playerdata.equipWeapon, unityChanStatus.GetItemNum(playerdata.equipWeapon += 1;
       // }
       // else
       // {
           // unityChanStatus.SetItemDictionary(playerdata.GetEquipWeapon(), 1);
       // }
       // unityChanStatus.SetEquipWeapon(playerdata.GetEquipWeapon());
       // unityChanStatus.CreatePage(playerdata.GetEquipPage());
       // unityChanStatus.SetSkillList(playerdata.GetEquipSkillList());
    }
}