using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;
using System.Linq;

public class Chat : MonobitEngine.MonoBehaviour
{
    [SerializeField] Text text;
    [SerializeField] InputField InputField;
    private string roomName = "";
    List<string> chatLog = new List<string>();
    void OnGUI()
    {
        // MUNサーバに接続している場合
        if (MonobitNetwork.isConnect)
        {
            // ルームに入室している場合
            if (MonobitNetwork.inRoom)
            {
                string msg = "";
                for (int i = 0; i < 10; ++i)
                {
                    msg += ((i < chatLog.Count) ? chatLog[i] : "") + "\r\n";
                }
                text.text = msg;
            }
        }
    }
    public void DearMessage()
    {
        monobitView.RPC("RecvChat",MonobitTargets.All,MonobitNetwork.playerName,InputField.text);
        InputField.text = "";
    }

    [MunRPC] void RecvChat(string senderName, string senderWord)
    {
        chatLog.Add(senderName + " : " + senderWord);
        if (chatLog.Count > 10)
        {
            chatLog.RemoveAt(0);
        }
    }

    
    
}
