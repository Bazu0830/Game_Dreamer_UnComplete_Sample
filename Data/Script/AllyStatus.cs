using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "AllyStatus", menuName = "CreateAllyStatus")]
public class AllyStatus : ScriptableObject
{
    // 自動でシーンオブジェクトに保存されます。初期化は別途
    [SerializeField]
    public string characterName = "";
    [SerializeField]
    public GameObject gameObject;
    [SerializeField]
    public string ObjectName="";
    [SerializeField][Multiline(10)]
    public string Information;
    [SerializeField]
    public Sprite Sprite;
    [SerializeField]
    public int star = 0;
    [SerializeField]
    public bool isPoisonState = false; //clay
    [SerializeField]
    public bool isNumbnessState = false; //wood
    [SerializeField]
    public bool isSlepe = false; //metal
    [SerializeField]
    public bool isbirned = false; //fire
    [SerializeField]
    public bool iswatered = false; //water
    [SerializeField]
    public int maxHp = 100;
    [SerializeField]
    public int hp = 100;
    [SerializeField]
    public int maxMp = 50;
    [SerializeField]
    public int mp = 50;
    [SerializeField]
    public int maxSt = 100;
    [SerializeField]
    public int st = 80;
    [SerializeField]
    public int PA = 5;
    [SerializeField]
    public int MA = 10;
    [SerializeField]
    public int PD = 10;
    [SerializeField]
    public int MD = 10;
    [SerializeField]
    public ItemDictionary1 itemDictionary = null;
    [SerializeField]
    public int money = 0;
    public enum Element
    {
        fire,
        water,
        wood,
        clay,
        metal
    }
    [SerializeField]
    public Element element = Element.fire;
   
}