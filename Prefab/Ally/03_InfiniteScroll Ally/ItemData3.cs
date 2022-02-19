/*
 * FancyScrollView (https://github.com/setchi/FancyScrollView)
 * Copyright (c) 2020 setchi
 * Licensed under MIT (https://github.com/setchi/FancyScrollView/blob/master/LICENSE)
 */

namespace FancyScrollView.Example033
{
    class ItemData3
    {
        public string Message { get; }

        public ItemData3(string message)
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
    class AllyData
    {
        public AllyStatus allyData { get; }

        public AllyData(AllyStatus item)
        {
            allyData = item;
        }
    }
}
