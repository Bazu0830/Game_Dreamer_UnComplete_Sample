using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
[CreateAssetMenu(fileName = "AllDictionaryData", menuName = "CreateAllDicData")]
public class AllDictionaryData : ScriptableObject
{
    [SerializeField] public AllyDictionary1 allydictionary;
    [SerializeField] public ItemDictionary1 itemdictionary;
    [SerializeField] public QuestDictionary questdictionary;
}
