using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Data
{
    //　パーティーステータスの保存データフィールド
    [SerializeField]
    private List<AllyStatus> partyStatusList;
    [SerializeField]
    private Vector3 position;
    //　ユニティちゃんの保存データフィールド
    [SerializeField]
    private string unityChanCharacterName = null;
    [SerializeField]
    private bool unityChanIsPoisonState;
    [SerializeField]
    private bool unityChanIsNumbnessState;
    [SerializeField]
    private int unityChanLevel;
    [SerializeField]
    private int unityChanMaxHP;
    [SerializeField]
    private int unityChanHP;
    [SerializeField]
    private int unityChanMaxMP;
    [SerializeField]
    private int unityChanMP;
    [SerializeField]
    private int unityChanPA;
    [SerializeField]
    private int unityChanMA;
    [SerializeField]
    private int unityChanPD;
    [SerializeField]
    private int unityChanMD;
    [SerializeField]
    private int unityChanLack;
    [SerializeField]
    private int unityChanMoney;
    [SerializeField]
    private int unityChanStatusPoint;
    [SerializeField]
    private int unityChanEarnedExperience;
    [SerializeField]
    private Item unityChanEquipWeapon = null;
    [SerializeField]
    private Item unityChanEquipArmor = null;


    [SerializeField]
    private ItemDictionary unityChanItemDictionary;
    [SerializeField]
    private List<Item> unityChanItemDictionaryKeys;
    [SerializeField]
    private List<int> unityChanItemDictionaryValues;

    public Vector3 Position
    {
        get { return position; }
        set { position = value; }
    }

    //　パーティーステータス保存データプロパティ
    public List<AllyStatus> PertyList
    {
        get { return partyStatusList; }
        set { partyStatusList = value; }
    }

    //　ユニティちゃん保存データプロパティ
    public string UnityChanCharacterName
    {
        get { return unityChanCharacterName; }
        set { unityChanCharacterName = value; }
    }
    public bool UnityChanIsPoisonState
    {
        get { return unityChanIsPoisonState; }
        set { unityChanIsPoisonState = value; }
    }
    public bool UnityChanIsNumbnessState
    {
        get { return unityChanIsNumbnessState; }
        set { unityChanIsNumbnessState = value; }
    }
    public int UnityChanLevel
    {
        get { return unityChanLevel; }
        set { unityChanLevel = value; }
    }
    public int UnityChanMaxHP
    {
        get { return unityChanMaxHP; }
        set { unityChanMaxHP = value; }
    }
    public int UnityChanHP
    {
        get { return unityChanHP; }
        set { unityChanHP = value; }
    }
    public int UnityChanMaxMP
    {
        get { return unityChanMaxMP; }
        set { unityChanMaxMP = value; }
    }
    public int UnityChanMP
    {
        get { return unityChanMP; }
        set { unityChanMP = value; }
    }
    public int UnityChanPA
    {
        get { return unityChanPA; }
        set { unityChanPA = value; }
    }
    public int UnityChanMA
    {
        get { return unityChanMA; }
        set { unityChanMA = value; }
    }
    public int UnityChanPD
    {
        get { return unityChanPD; }
        set { unityChanPD = value; }
    }
    public int UnityChanMD
    {
        get { return unityChanMD; }
        set { unityChanMD = value; }
    }
    public int UnityChanLack
    {
        get { return unityChanLack; }
        set { unityChanLack = value; }
    }
    public int UnityChanEarnedExperience
    {
        get { return unityChanEarnedExperience; }
        set { unityChanEarnedExperience = value; }
    }
    public int UnityChanMoney
    {
        get { return unityChanMoney; }
        set { unityChanMoney = value; }
    }
    public int UnityChanStausPoint
    {
        get { return unityChanStatusPoint; }
        set { unityChanStatusPoint = value; }
    }
    public Item UnityChanEquipWeapon
    {
        get { return unityChanEquipWeapon; }
        set { unityChanEquipWeapon = value; }
    }
    public Item UnityChanEquipArmor
    {
        get { return unityChanEquipArmor; }
        set { unityChanEquipArmor = value; }
    }

    public ItemDictionary UnityChanItemDictionary
    {
        get { return unityChanItemDictionary; }
        set { unityChanItemDictionary = value; }
    }
    public List<Item> UnityChanItemDictionaryKeys
    {
        get { return unityChanItemDictionaryKeys; }
        set { unityChanItemDictionaryKeys = value; }
    }
    public List<int> UnityChanItemDictionaryValues
    {
        get { return unityChanItemDictionaryValues; }
        set { unityChanItemDictionaryValues = value; }
    }

}