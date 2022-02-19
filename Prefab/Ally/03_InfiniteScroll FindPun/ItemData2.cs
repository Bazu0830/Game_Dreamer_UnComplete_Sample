/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

namespace FancyScrollView.Example032
{
    class ItemData2
    {
        public string Message { get; }

        public ItemData2(string message)
        {
            Message = message;
        }
    }
    class QuestData2
    {
        public Quest Quest { get; }

        public QuestData2(Quest quest)
        {
            Quest = quest;
        }
    }
    class RoomInfo
    {
        public string SecretRoomID { get; }
        public int PlayerCount { get; }
        public int MaxPlayerCount { get; }
        public string Stage { get; }
        public string Passward { get; }
        public string Roomname { get; }
        public string Difficulty { get; }
        public string Username { get; }
        public string PvP { get; }
        public string Time { get; }
        public string Questname { get; }

        public RoomInfo(string secretroomID, int playercount,int maxplayercount,string stage,
            string passward, string roomname, string difficulty, string username,
            string pvP, string time, string questname)
        {
            SecretRoomID = secretroomID;
            PlayerCount = playercount;
            MaxPlayerCount = maxplayercount;
            Stage = stage;
            Passward = passward;
            Roomname = roomname;
            Difficulty = difficulty;
            Username = username;
            PvP = pvP;
            Time = time;
            Questname = questname;
        }
    }
}
