using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using FancyScrollView.Example032;
using System;
using System.Linq;
using MonobitEngine;

public class MonobitManager : MonobitEngine.MunMonoBehaviour
{
    #region 部屋作成変数
                     public Quest quest;
    [SerializeField] int maxPlayers = 4;
    [SerializeField] bool isVisible = true;
    [SerializeField] bool isOpen = true;
    [SerializeField] string roomName = "誰でも";
    [SerializeField] string stageName = "Stage1";
    [SerializeField] string stageDifficulty = "Easy";
    [SerializeField] string SecretRoomID = "";
    [SerializeField] string Key = "";
    [SerializeField] string PvP = "";
    [SerializeField] string questName = "";
    [SerializeField] PlayerData playerdata;
    [SerializeField] InputField RoomNameInput;
    [SerializeField] InputField RoomKeyInput;
    [SerializeField] Text MaxplayerText;
    [SerializeField] Toggle PvPToggle;
    [SerializeField] Text PvPText;
    [SerializeField] GameObject FindContent;
    [SerializeField] GameObject NetworkPrefab;
    [SerializeField] InputField IDKey;

    [SerializeField] GameObject NowLoadingPanel;
    [SerializeField] Slider NowLoadingSlider;
    private AsyncOperation async;
    [SerializeField] Example032 example032;
    [SerializeField] AllDictionaryData alldata;

    private DateTime dateTime;

    private List<RoomInfo> roominfoList = new List<RoomInfo>();
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        if (!MonobitNetwork.isConnect)
        {
            MonobitNetwork.ConnectServer("v1.0");
            MonobitNetwork.autoJoinLobby = true;
            Debug.Log("Photonに接続しました。");
        }
    }
    void Update()
    {
        if (playerdata.QuestDifficulty==PlayerData.questDifficulty.Easy)
        {
            stageDifficulty = "Easy";
        }
        else if (playerdata.QuestDifficulty == PlayerData.questDifficulty.Normal)
        {
            stageDifficulty = "Normal";
        }
        else if (playerdata.QuestDifficulty == PlayerData.questDifficulty.Hard)
        {
            stageDifficulty = "Hard";
        }
        else if (playerdata.QuestDifficulty == PlayerData.questDifficulty.Expert)
        {
            stageDifficulty = "Expert";
        }
        else 
        {
            stageDifficulty = "Master";
        }
        if (quest!=null)
        {
            stageName = quest.StageName;
            questName = alldata.questdictionary.FirstOrDefault(c => c.Key == quest).Value.ToString();
        }


        if (PvPToggle.isOn == true)
        {
            PvPText.text = "有り";
            PvP = "有り";
        }
        else if (PvPToggle.isOn == false)
        {
            PvPText.text = "無し";
            PvP = "無し";
        }

        MaxplayerText.text = maxPlayers.ToString();
        //部屋人数Sliderの値をTextに代入
        roomName = RoomNameInput.text;
        Key = RoomKeyInput.text;
    }

    public void RundomMatch()
    {
        if (MonobitNetwork.isConnect)
        {
            if (!MonobitNetwork.inRoom)
            {
                // ルーム設定
                Hashtable roomOptions = new Hashtable() { { "questname", "RandomRoom" } };
                MonobitNetwork.JoinRandomRoom(roomOptions,0);
            }
        }
    }
    public void CreateRoom()
    {
        if (MonobitNetwork.isConnect)
        {
            if (!MonobitNetwork.inRoom)
            {
                SecretRoomID = StringUtils.GeneratePassword(8);

                // ルーム設定
                MonobitEngine.RoomSettings roomOptions = new RoomSettings() { 
                isVisible = true,
                isOpen = true,
                maxPlayers = (byte)maxPlayers,
                roomParameters= new Hashtable() { { "password", "" }, { "roomName", "" },
                { "stage", "" }, { "difficulty", "" }, { "username", "" }, { "PvP", "" },
                { "Time", "" }, { "questname", "" },},
                };
                
                roomOptions.roomParameters["password"] = Key;
                roomOptions.roomParameters["roomName"] = roomName;
                roomOptions.roomParameters["stage"] = stageName;
                roomOptions.roomParameters["difficulty"] = stageDifficulty;
                roomOptions.roomParameters["username"] = playerdata.Name;
                roomOptions.roomParameters["PvP"] = PvP;
                roomOptions.roomParameters["Time"] = (dateTime.Year * 10000000000 + dateTime.Month * 100000000 + dateTime.Day * 1000000 + dateTime.Hour * 10000 + dateTime.Minute * 100 + dateTime.Second).ToString();
                roomOptions.roomParameters["questname"] = questName;

                // ロビーに公開するカスタムプロパティを指定
                roomOptions.lobbyParameters = new string[] { "password", "roomName", "stage", "difficulty", "username", "PvP", "Time", "questname" };
                //部屋を開ける
                MonobitNetwork.CreateRoom(SecretRoomID, roomOptions, null);
            }
        }
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
        NowLoadingPanel.SetActive(true);
        StartCoroutine("LoadData");
    }

    public void MaxplayerUp()
    {
        if (maxPlayers < 4)
        {
            maxPlayers++;
        }
    }
    public void MaxplayerDown()
    {
        if (maxPlayers > 1)
        {
            maxPlayers--;
        }
    }

    IEnumerator LoadData()
    {

        async = SceneManager.LoadSceneAsync("One");

        //　読み込みが終わるまで進捗状況をスライダーの値に反映させる
        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            NowLoadingSlider.value = progressVal;
            yield return null;
        }
    }
    public void GetRoomINfo()
    {
        foreach (RoomData info in MonobitEngine.MonobitNetwork.GetRoomData())
        {
            RoomInfo roomInfo1 = new RoomInfo(info.name, info.playerCount, (int)info.maxPlayers,
                info.parametersListedInLobby[2], info.parametersListedInLobby[0], 
                info.parametersListedInLobby[1], info.parametersListedInLobby[3],
                info.parametersListedInLobby[4], info.parametersListedInLobby[5],
                info.parametersListedInLobby[6], info.parametersListedInLobby[7]);

            roominfoList.Add(roomInfo1);
        }
        example032.quests = roominfoList;
        example032.Start();
    }
    #region ランダムな値を取る。
    public static class StringUtils
    {
        private const string PASSWORD_CHARS =
            "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static string GeneratePassword(int length)
        {
            var sb = new System.Text.StringBuilder(length);
            var r = new System.Random();

            for (int i = 0; i < length; i++)
            {
                int pos = r.Next(PASSWORD_CHARS.Length);
                char c = PASSWORD_CHARS[pos];
                sb.Append(c);
            }

            return sb.ToString();
        }
    }
    #endregion

    public void RoomIDJoinRoom()
    {
        if (IDKey != null)
        {
            //roomnameの部屋に入室
            MonobitNetwork.JoinRoom(IDKey.text);
        }
    }

    public override void OnDisconnectedFromServer()
    {
        MonobitEngine.MonobitNetwork.offline = true;
    }
}
