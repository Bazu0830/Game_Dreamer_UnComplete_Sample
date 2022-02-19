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
 
namespace FancyScrollView.Example03
{
    class Example03 : MonoBehaviour
    {
        [SerializeField] MonobitManager monobit = default;
        [SerializeField] PlayerData PlayerData = default;
        [SerializeField] AllDictionaryData alldata = default;
        [SerializeField] ScrollView scrollView = default;
        private List<Quest> quest=new List<Quest>();
        private List<Quest> quest2=new List<Quest>();
        private List<Quest> quests=new List<Quest>();
        [SerializeField] Image image = default;
        [SerializeField] Text selecttext = default;
        [SerializeField] Text selectinfo = default;

        [SerializeField] GameObject SortPanel = default;
        [SerializeField] GameObject FilterPanel = default;
        [SerializeField] GameObject GoPanel = default;
        [SerializeField] GameObject StartPanel = default;

        private int difficults=0;
        private int selects = 0;
        private int sorts=0;
        private int filters=0;

        private void Awake()
        {
            for(int i=0; i < PlayerData.quests.Count; i++)
            {
                int a = PlayerData.quests[i];
                Quest b = alldata.questdictionary.FirstOrDefault(c => c.Value == a).Key;
                quest.Add(b);
                quest2.Add(b);
                quests.Add(b);
            }
        }
        void Start()
        {
            SortPanel.SetActive(false);
            FilterPanel.SetActive(false);
            GoPanel.SetActive(false);
            StartPanel.SetActive(false);

            SetSelects(selects);
            SetFilters(filters);
            SetSort();
           
            scrollView.OnSelectionChanged(OnSelectionChanged);

            var items = Enumerable.Range(0, quests.Count)
                .Select(i => new QuestData(quests[i]))
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }
        void OnSelectionChanged(int index)
        {
            selecttext.text = quests[index].QuestName;
            image.sprite = quests[index].icon;
            selectinfo.text = quests[index].information;
            monobit.quest = quests[index];
        }

        public void GopanelOpen()
        {
            GoPanel.SetActive(true);
        }
        public void GopanelClose()
        {
            GoPanel.SetActive(false);
        }
        public void StartpanelOpen()
        {
            StartPanel.SetActive(true);
        }
        public void StartpanelClose()
        {
            StartPanel.SetActive(false);
        }

        public void SortPanelOpen()
        {
            SortPanel.SetActive(true);
        }
        public void FilterPanelOpen()
        {
            FilterPanel.SetActive(true);
        }

        public void SetSort()
        {
            if (sorts == 1)
            {
                if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Easy)
                {
                    quests.Sort((a, b) => b.EasyScore - a.EasyScore);
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Normal)
                {
                    quests.Sort((a, b) => b.NormalScore - a.NormalScore);
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Hard)
                {
                    quests.Sort((a, b) => b.HardScore - a.HardScore);
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Expert)
                {
                    quests.Sort((a, b) => b.ExpertScore - a.ExpertScore);
                }
                else
                {
                    quests.Sort((a, b) => b.MasterScore - a.MasterScore);
                }
            }
            else if (sorts == 2)
            {
                quests.Sort((a, b) => a.difficultylevel - b.difficultylevel);
            }
            else
            {
                quests.Sort((a, b) => (a.ReleaseNum * 100 + a.StoryNum) - (b.ReleaseNum * 100 + b.StoryNum));
            }
        }

        public void SetSelects(int select)
        {
            if (select != 0)
            {
                quest2 = new List<Quest>(quest.FindAll((a) => a.ReleaseNum == select));
            }
            else
            {
                quest2 = new List<Quest>(quest);
            }
        }

        public void SetFilters(int filter)
        {
            if (filter == 1)
            {
                if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Easy)
                {
                    quests = new List<Quest>(quest2.FindAll((a) =>a.EasyScore == 0));
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Normal)
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.NormalScore == 0));
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Hard)
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.HardScore == 0));
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Expert)
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.ExpertScore == 0));
                }
                else
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.MasterScore == 0));
                }
            }

            else if (filter == 3)
            {
                if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Easy)
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.EasyScore < 100000));
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Normal)
                {
                    quests = new List<Quest>(quest2.FindAll((a) =>a.NormalScore < 100000));
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Hard)
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.HardScore < 100000));
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Expert)
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.ExpertScore < 100000));
                }
                else
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.MasterScore < 100000));
                }
            }
            else if (filter == 4)
            {
                if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Easy)
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.isStory == true));
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Normal)
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.isStory == true));
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Hard)
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.isStory == true));
                }
                else if (PlayerData.QuestDifficulty == PlayerData.questDifficulty.Expert)
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.isStory == true));
                }
                else
                {
                    quests = new List<Quest>(quest2.FindAll((a) => a.isStory == true));
                }
            }
            else
            {
                quests = new List<Quest>(quest2);
            }
        }

       
        public void SortDefault()
        {
            sorts = 0;
            Start();
        }
        public void SortHiScore()
        {
            sorts = 1;
            Start();
        }
        public void SortDifficulty()
        {
            sorts = 2;
            Start();
        }

       
        public void SetEasy()
        {
            difficults = 0;
            PlayerData.QuestDifficulty = PlayerData.questDifficulty.Easy;
            Start();
        }
        public void SetNormal()
        {
            difficults= 1;
            PlayerData.QuestDifficulty = PlayerData.questDifficulty.Normal;
            Start();
        }
        public void SetHard()
        {
            difficults = 2;
            PlayerData.QuestDifficulty = PlayerData.questDifficulty.Hard;
            Start();
        }
        public void SetExpert()
        {
            difficults = 3;
            PlayerData.QuestDifficulty = PlayerData.questDifficulty.Expert;
            Start();
        }
        public void SetMaster()
        {
            difficults = 4;
            PlayerData.QuestDifficulty = PlayerData.questDifficulty.Master;
            Start();
        }
        public void SelectList(int select)
        {
            selects = select;
            Start();
        }
       
        public void SetFilter(int filter)
        {
            filters = filter;
            Start();
        }
       
    }
}
