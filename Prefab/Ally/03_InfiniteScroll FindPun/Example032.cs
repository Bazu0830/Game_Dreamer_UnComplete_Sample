/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MonobitEngine;
using UnityEngine.SceneManagement;

namespace FancyScrollView.Example032
{
    class Example032 : MonobitEngine.MonoBehaviour
    {
        [SerializeField] PlayerData PlayerData = default;
        [SerializeField] ScrollView2 scrollView = default;
        public List<RoomInfo> quests;
        public List<RoomInfo> quest;
        public List<RoomInfo> questmain;
        [SerializeField] Text text;

        [SerializeField] InputField KeyInput;
        private string Key;
        private int MemberCount;
        private int MaxCount;
        private string roomname;
        private string difficulty;
        private string pvp;
        private string playername;
        private string keybool;
        private string stage;
        private string time;
        //入室ボタンroomname格納用
        private string SecretRoomID;

        [SerializeField] GameObject FilterPanel = default;
        [SerializeField] InputField roomID = default;
        [SerializeField] InputField roomName = default;
        [SerializeField] InputField questName = default;
        private int difficult = 0;
        private int sort = 0;
        private int filter = 0;



        private void Awake()
        {
            quests = new List<RoomInfo>();
            quest = new List<RoomInfo>(quests);
            questmain = new List<RoomInfo>(quests);
        }
        public void Start()
        {
            if (quests.Count == 0)
            {
                return;
            }

            FilterPanel.SetActive(false);
            quest = new List<RoomInfo>(quests);
            questmain = new List<RoomInfo>(quests);
            scrollView.OnSelectionChanged(OnSelectionChanged);
            Difficulty(difficult);
            Filters(filter);
            Sorts(sort);
            if (roomID.text != "")
            {
                quests = new List<RoomInfo>(questmain.FindAll((a) => a.SecretRoomID == roomID.text));
            }
            if(roomName.text != "")
            {
                quests = new List<RoomInfo>(quests.FindAll((a) => a.Roomname.Contains(roomName.text)));
            }
            if(questName.text != "")
            {
                quests = new List<RoomInfo>(quests.FindAll((a) => a.Questname.Contains(questName.text)));
            }

            var items = Enumerable.Range(0, quests.Count)
                .Select(i => quests[i])
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }
        void OnSelectionChanged(int index)
        {
            text.text = quests[index].Roomname;
            SecretRoomID = quests[index].SecretRoomID;
            MemberCount = quests[index].PlayerCount;
            MaxCount = quests[index].MaxPlayerCount;
            stage = quests[index].Stage;
            roomname = quests[index].Roomname;
            difficulty = quests[index].Difficulty;
            playername = quests[index].Username;
            Key = quests[index].Passward;
            pvp = quests[index].PvP;
            time = quests[index].Time;

            if (Key == "")
            {
                keybool = "無";
            }
            else
            {
                keybool = "有";
            }
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
            SceneManager.LoadScene("one");
        }

        public void FilterPanelOpen()
        {
            FilterPanel.SetActive(true);
        }
        public void FilterStart()
        {
            FilterPanel.SetActive(false);



            Start();
        }
        public void SetSort(int sorts)
        {
            sort = sorts;
        }

        public void SetFilter(int filters)
        {
            filter = filters;
        }
        public void Setdifficult(int difficulty)
        {
            difficult = difficulty;
        }

        public void Sorts(int sorts)
        {
            if (sorts == 1)
            {
                quests.Sort((a, b) => (b.MaxPlayerCount-b.PlayerCount) - (a.MaxPlayerCount-a.PlayerCount));
            }
            else
            {
                quests.Sort((a, b) => int.Parse(b.Time) - int.Parse(a.Time));
            }
        }
        public void Filters(int filters)
        {
            if (filters == 1)
            {
                quests = new List<RoomInfo>(quest.FindAll((a) => a.Passward == ""));
            }
            else
            {
                quests = new List<RoomInfo>(quest);
            }
        }
        public void Difficulty(int difficult)
        {
            if (difficult == 1)
            {
                quest = new List<RoomInfo>(quest.FindAll((a) => a.Difficulty == "Easy"));
            }
            else if (difficult == 2)
            {
                quest = new List<RoomInfo>(quest.FindAll((a) => a.Difficulty == "Normal"));
            }
            else if (difficult == 3)
            {
                quest = new List<RoomInfo>(quest.FindAll((a) => a.Difficulty == "Hard"));
            }
            else if (difficult == 4)
            {
                quest = new List<RoomInfo>(quest.FindAll((a) => a.Difficulty == "Expert"));
            }
            else if (difficult == 5)
            {
                quest = new List<RoomInfo>(quest.FindAll((a) => a.Difficulty == "Master"));
            }
            else 
            {
                quest = new List<RoomInfo>();
            }
        }
    }
}
