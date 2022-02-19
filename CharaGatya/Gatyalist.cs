using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "GatyaData", menuName = "CreateGatyaData")]
public class Gatyalist : ScriptableObject
{
    [Header("金")]
    [SerializeField]
    private AllyDictionary1 gold = null;
    [Header("銀")]
    [SerializeField]
    private AllyDictionary1 silver = null;
    [Header("銅")]
    [SerializeField]
    private AllyDictionary1 bronze = null;
    public void CreateGold(AllyDictionary1 gold)
    {
        this.gold = gold;
    }

    public void SetGold(AllyStatus ally, int num = 0)
    {
        gold.Add(ally, num);
    }
    //　アイテムが登録された順番のItemDictionaryを返す
    public AllyDictionary1 GetGold()
    {
        return gold;
    }
    public void CreateSilver(AllyDictionary1 silver)
    {
        this.silver = silver;
    }

    public void SetSilver(AllyStatus ally, int num = 0)
    {
        silver.Add(ally, num);
    }
    //　アイテムが登録された順番のItemDictionaryを返す
    public AllyDictionary1 GetSilver()
    {
        return silver;
    }
    public void CreateBronze(AllyDictionary1 bronze)
    {
        this.bronze = bronze;
    }

    public void SetBronze(AllyStatus ally, int num = 0)
    {
        bronze.Add(ally, num);
    }
    //　アイテムが登録された順番のItemDictionaryを返す
    public AllyDictionary1 GetBronze()
    {
        return bronze;
    }
}
