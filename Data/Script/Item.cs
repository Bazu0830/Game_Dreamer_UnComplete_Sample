using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "CreateItem")]
public class Item : ScriptableObject
{
    public enum Type
    {
        MaxHpUp,
        MaxMpUp,
        MaxStUp,
        HPRecovery,
        MPRecovery,
        StRecovery,
        PoisonRecovery,
        NumbnessRecovery,
        WaterRecovery,
        SleepRecovery,
        BirnedRecovery,
        PAUp,
        PDUp,
        MAUp,
        MDUp,
        BigMoney,
        RandomJackpot,
        NoneWeapon,
        WeaponSword,
        WeaponBow,
        WeaponPlate,
        MagicBook,
        Valuables,
        Key
    }

    //　アイテムの種類
    [SerializeField]
    public Type itemType = Type.HPRecovery;
    //　アイテムの漢字名
    [SerializeField]
    private string kanjiName = "";
    //　アイテムの平仮名名
    [SerializeField]
    private string hiraganaName = "";
    //　アイテム情報
    [SerializeField][Multiline(10)]
    private string information = "";
    //　アイテムのパラメータ
    [SerializeField]
    private int amount = 0;
    [SerializeField]
    private Sprite icon;

    //　アイテムの種類を返す
    public Type GetItemType()
    {
        return itemType;
    }
    //　アイテムの名前を返す
    public string GetKanjiName()
    {
        return kanjiName;
    }
    //　アイテムの平仮名の名前を返す
    public string GetHiraganaName()
    {
        return hiraganaName;
    }
    //　アイテム情報を返す
    public string GetInformation()
    {
        return information;
    }
    //　アイテムの強さを返す
    public int GetAmount()
    {
        return amount;
    }
    public Sprite GetSprite()
    {
        return icon;
    }
    //　アイテムのオブジェクトを返す

}