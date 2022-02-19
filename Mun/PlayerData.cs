using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[Serializable]
[CreateAssetMenu(fileName = "PlayerData", menuName = "CreatePlayerData")]
public class PlayerData : ScriptableObject
{
    [SerializeField]
    public string Name = "";
    [SerializeField]
    public string ID = "";
    [SerializeField]
    public AllyStatus ally;
    [SerializeField]
    public AllyStatus ally2;
    [SerializeField]
    public AllyStatus ally3;
    [SerializeField]
    public AllyStatus ally4;
    [SerializeField]
    public int rank=1;
    [SerializeField]
    public int gatya =0;
    [SerializeField]
    public int medal = 0;
    [SerializeField]
    public int star = 0;
    [SerializeField]
    public AllyDictionary allydictionary;
    //　アイテムと個数のDictionary
    [SerializeField]
    public ItemDictionary itemDictionary = null;
    [SerializeField]
    public List<int> quests = null;
    public enum questDifficulty
    {
        Easy,
        Normal,
        Hard,
        Expert,
        Master
    }
    [SerializeField]
    public questDifficulty QuestDifficulty = questDifficulty.Easy;
}
