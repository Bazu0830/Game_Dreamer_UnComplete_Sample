using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemDictionary))]
[CustomPropertyDrawer(typeof(AllyDictionary))]
[CustomPropertyDrawer(typeof(QuestDictionary))]
[CustomPropertyDrawer(typeof(AllyDictionary1))]
[CustomPropertyDrawer(typeof(ItemDictionary1))]

public class SerializableDictionary : SerializableDictionaryPropertyDrawer
{

}