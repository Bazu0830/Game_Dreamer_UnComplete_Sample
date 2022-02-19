using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCTalkingCanvas : MonoBehaviour
{
    [SerializeField]
    MenuOpen menuopen;
    [TextArea(1, 100)] public string npctalking;
    [SerializeField]
    GameObject talkmessage;
    [SerializeField]
    NPCTalking nPCTalking;

    public void NPCTalkingOn()
    {
        menuopen.TalkPanel();
        nPCTalking.talkall = npctalking;
        nPCTalking.SetMessage();
        talkmessage.SetActive(true);
        

    }
    public void NPCTalkingOff()
    {
        talkmessage.SetActive(false);
        menuopen.ToGame();
    }
}
