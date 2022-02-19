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

namespace FancyScrollView.Example033
{
    class Example033 : MonoBehaviour
    {
        [SerializeField] AllDictionaryData alldata;
        [SerializeField] PlayerData PlayerData = default;
        [SerializeField] ScrollView3 scrollView = default;
        public List<AllyStatus> quests = new List<AllyStatus>();
        public List<AllyStatus> quest = new List<AllyStatus>();
        public List<AllyStatus> questmain = new List<AllyStatus>();
        public List<int> nums = new List<int>();
        public List<int> num = new List<int>();
        public List<int> nummain = new List<int>();
        [SerializeField] Text Nametext;
        [SerializeField] Text Numtext;
        [SerializeField] Text Infotext;


        [SerializeField] InputField ItemName = default;
        private int difficult = 0;
        private int sort = 0;
        private int filter = 0;
        public AllyStatus Ally;
        public int allynum;



        public void First()
        {
            quest = new List<AllyStatus>();
            questmain = new List<AllyStatus>();
            quests = new List<AllyStatus>();
            num = new List<int>();
            nummain = new List<int>();
            nums = new List<int>();


            foreach (KeyValuePair<int, int> kvp in PlayerData.allydictionary)
            {
                int a = kvp.Key;
                AllyStatus b = alldata.allydictionary.FirstOrDefault(d => d.Value == a).Key;
                int c = kvp.Value;
                quest.Add(b);
                questmain.Add(b);
                quests.Add(b);
                num.Add(c);
                nummain.Add(c);
                nums.Add(c);
            }
        }
        public void Second()
        {
            quest = new List<AllyStatus>(quests);
            questmain = new List<AllyStatus>(quests);
            scrollView.OnSelectionChanged(OnSelectionChanged);
            Difficulty(difficult);
            Filters(filter);
            Sorts(sort);
            if (ItemName.text != "")
            {
                quests = new List<AllyStatus>(questmain.FindAll((a) => a.characterName == ItemName.text));
            }


            var items = Enumerable.Range(0, quests.Count)
                .Select(i => new AllyData(quests[i]))
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }
        void OnSelectionChanged(int index)
        {
            Nametext.text = quests[index].characterName;
            Numtext.text = nums[index].ToString();
            Infotext.text = quests[index].Information;
            Ally = quests[index];
        }

        public void SetAllyNum(int i)
        {
            allynum = i;
        }

        //入室ボタン処理
        public void EquipAlly()
        {
            if (allynum == 1)
            {
                PlayerData.ally = Ally;
                if (Ally == PlayerData.ally2)
                {
                    PlayerData.ally2 = null;
                }
                if (Ally == PlayerData.ally3)
                {
                    PlayerData.ally3 = null;
                }
                if (Ally == PlayerData.ally4)
                {
                    PlayerData.ally4 = null;
                }
            }
            else if (allynum == 2)
            {
                PlayerData.ally2 = Ally;
                if (Ally == PlayerData.ally)
                {
                    PlayerData.ally = null;
                }
                if (Ally == PlayerData.ally3)
                {
                    PlayerData.ally3 = null;
                }
                if (Ally == PlayerData.ally4)
                {
                    PlayerData.ally4 = null;
                }
            }
            else if (allynum == 3)
            {
                PlayerData.ally3 = Ally;
                if (Ally == PlayerData.ally)
                {
                    PlayerData.ally = null;
                }
                if (Ally == PlayerData.ally2)
                {
                    PlayerData.ally2 = null;
                }
                if (Ally == PlayerData.ally4)
                {
                    PlayerData.ally4 = null;
                }
            }
            else
            {
                PlayerData.ally4 = Ally;
                if (Ally == PlayerData.ally)
                {
                    PlayerData.ally = null;
                }
                if (Ally == PlayerData.ally2)
                {
                    PlayerData.ally2 = null;
                }
                if (Ally == PlayerData.ally3)
                {
                    PlayerData.ally3 = null;
                }
            }
        }

        
        public void FilterStart()
        {
            



            Second();
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

        }
        public void Filters(int filters)
        {

        }
        public void Difficulty(int difficult)
        {

        }

    }
}
