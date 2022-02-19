using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "Quest", menuName = "CreateQuest")]
public class Quest : ScriptableObject
{
    [SerializeField] public int ReleaseNum = 1;
    [SerializeField] public int StoryNum = 1;
    [SerializeField] public string QuestName = "百鬼夜行";
    [SerializeField] [Multiline(10)] public string information =
        "起き上がるとそこには、" +
        "変貌を遂げた父の姿が" +
        "そこにはあった。";
    [SerializeField] public int difficultylevel = 21;
    [SerializeField] public Sprite icon;
    [SerializeField] public bool isStory;
    [SerializeField] public string StageName = "Forest 1";
    [SerializeField] public int EasyScore;
    [SerializeField] public int NormalScore;
    [SerializeField] public int HardScore;
    [SerializeField] public int ExpertScore;
    [SerializeField] public int MasterScore;
    //URL
    [SerializeField] public string IconURL;
    [SerializeField] public string StartStoryURL;
    [SerializeField] public string EndStoryURL;

}
