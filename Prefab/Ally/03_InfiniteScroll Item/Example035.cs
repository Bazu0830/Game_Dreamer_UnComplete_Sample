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

namespace FancyScrollView.Example035
{
    class Example035 : MonoBehaviour
    {
        [SerializeField] AllDictionaryData alldata;
        [SerializeField] PlayerData PlayerData = default;
        [SerializeField] ScrollView5 scrollView = default;
        public List<Item> quests = new List<Item>();
        public List<int> nums=new List<int>();
        [SerializeField] Text Nametext;
        [SerializeField] Text Numtext;
        [SerializeField] Text Infotext;


        [SerializeField] GameObject FilterPanel = default;
        [SerializeField] InputField ItemName = default;
        private int difficult = 0;
        private int sort = 0;
        private int filter = 0;

        private Item items;

        [SerializeField] Example033.Example033 example;

        public void First()
        {
            quests=new List<Item>();
            nums=new List<int>();

            foreach (KeyValuePair<int, int> kvp in PlayerData.itemDictionary)
            {
                int a = kvp.Key;
                Item b = alldata.itemdictionary.FirstOrDefault(d => d.Value == a).Key;
                int c = kvp.Value;
                quests.Add(b);
                nums.Add(c);
            }
        }
        public void Second()
        {
            FilterPanel.SetActive(false);
            scrollView.OnSelectionChanged(OnSelectionChanged);
            Difficulty(difficult);
            Filters(filter);
            Sorts(sort);
            if (ItemName.text != "")
            {
                quests = new List<Item>(quests.FindAll((a) => a.GetKanjiName() == ItemName.text));
            }


            var items = Enumerable.Range(0, quests.Count)
                .Select(i => new ItemData(quests[i]))
                .ToArray();

            scrollView.UpdateData(items);
            scrollView.SelectCell(0);
        }
        void OnSelectionChanged(int index)
        {
            Nametext.text = quests[index].GetKanjiName();
            Numtext.text = nums[index].ToString();
            Infotext.text = quests[index].GetInformation();
            items = quests[index];
        }

        public void FilterPanelOpen()
        {
            FilterPanel.SetActive(true);
        }
        public void FilterStart()
        {
            
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
            if(filters == 1)
            {
                
            }
        }
        public void Difficulty(int difficult)
        {
            
        }
    }
}
