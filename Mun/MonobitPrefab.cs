using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using MonobitEngine;

public class MonobitPrefab : MonobitEngine.MonoBehaviour
{
    //Room情報UI表示用
    [SerializeField] public Text RoomInfo;
    [SerializeField] private string Key;
    [SerializeField] private int MemberCount;
    [SerializeField] private int MaxCount;
    [SerializeField] private InputField KeyInput;
    private string keybool;
    private string stage;
    //入室ボタンroomname格納用
    private string SecretRoomID;

    //GetRoomListからRoom情報をRoomElementにセットしていくための関数
    public void SetRoomInfo(string _RoomID, int _PlayerNumber, int _MaxPlayer,
        string _RoomName, string _Stage, string _Difficulty, string _Key, string _Player, string _PvP)
    {
        //入室ボタン用roomname取得
        SecretRoomID = _RoomID;
        Key = _Key;
        MemberCount = _PlayerNumber;
        MaxCount = _MaxPlayer;
        stage = _Stage;
        if (Key == "")
        {
            keybool = "無";
        }
        else
        {
            keybool = "有";
        }
        RoomInfo.text = "部屋名:" + _RoomName + "  親:" + _Player + "\n" + "人数:" + _PlayerNumber + "/" + _MaxPlayer + "  舞台:" + _Stage + "  難易度:" + _Difficulty + "  PvP:" + _PvP + "  鍵:" + keybool;
    }

    //入室ボタン処理
    public void OnJoinRoomButton()
    {
        if (Key == ""
            && MemberCount < MaxCount)
        {
            //roomnameの部屋に入室
            MonobitNetwork.JoinRoom(SecretRoomID);
        }
        else
        {
            if (KeyInput.text == Key
                && MemberCount < MaxCount)
            {
                //roomnameの部屋に入室
                MonobitNetwork.JoinRoom(SecretRoomID);
            }
            else
            {
                Debug.Log("鍵が違う。または人数制限に触れました");
            }
        }
    }
    public void OnJoinedRoom()
    {
        if (stage == "第一章")
        {
            SceneManager.LoadScene("OnlineRoom");
        }
        else if (stage == "闘技場")
        {
            SceneManager.LoadScene("one");
        }
    }
}
