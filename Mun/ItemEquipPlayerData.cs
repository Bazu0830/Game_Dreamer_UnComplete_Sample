using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEquipPlayerData : MonoBehaviour
{
    [SerializeField]
    GameObject itembutton;
    [SerializeField]
    GameObject iapitembutton;
    [SerializeField]
    public PlayerData playerdata;

    private Item item;
    //　データをセットする
    public void SetParam(Item item)
    {
        this.item = item;
    }

    //　装備する
    public void Equip()
    {
        if (item.GetItemType() == Item.Type.NoneWeapon
          || item.GetItemType() == Item.Type.WeaponSword
          || item.GetItemType() == Item.Type.WeaponBow
          || item.GetItemType() == Item.Type.WeaponPlate)
        {
            Debug.Log("武器を装備した");
        }
        else
        {
            Debug.Log("装備できない");
        }
    }
}
