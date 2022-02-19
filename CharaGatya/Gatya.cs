using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Gatya : MonoBehaviour
{
    [SerializeField]
    AllDictionaryData alldata;
    [SerializeField]
    GameObject GatyaPanel;
    [SerializeField]
    Gatyalist gatyalist;
    [SerializeField]
    PlayerData playerdata;
    AllyStatus ally;
    [SerializeField]
    public Text text1;
    [SerializeField]
    public Text text2;
    [SerializeField]
    public Text text3;
    [SerializeField]
    GameObject ResultPanel;
    [SerializeField]
    Text resulttext;
    private AllyDictionary1 allys=new AllyDictionary1();

    private void Start()
    {
        foreach (KeyValuePair<int,int> kvp in playerdata.allydictionary)
        {
            int a = kvp.Key;
            AllyStatus b = alldata.allydictionary.FirstOrDefault(d => d.Value == a).Key;
            int c = kvp.Value;
            allys.Add(b,c);
        }
    }
    private void OnGUI()
    {
        text1.text = "魔書の断片：" + playerdata.gatya + "個";
        text2.text = "魔書の断片：" + playerdata.gatya + "個";
        text3.text = "魔書の断片：" + playerdata.gatya + "個";
    }
    public void GatyaClose()
    {
        GatyaPanel.SetActive(false);
    }
    public void GatyaOpen()
    {
        GatyaPanel.SetActive(true);
    }
    public void GatyaTen()
    {
        if (playerdata.gatya >= 50)
        {
            playerdata.gatya -= 50;
            playerdata.medal += 10;
            for (int i = 0; i < 10; ++i)
            {
                GatyaSingle();
            }
            GatyaBonus();
        }
        else
        {
            Debug.Log("ガチャ券が足りません");
            resulttext.text = "ガチャ券が足りません";
        }
    }
    public void GatyaOnce()
    {
        if (playerdata.gatya >= 5)
        {
            playerdata.gatya -= 5;
            playerdata.medal += 1;
            GatyaSingle();
        }
        else
        {
            Debug.Log("ガチャ券が足りません");
            resulttext.text = "ガチャ券が足りません";
        }
    }
    public void GatyaSingle()
    {
        int random = Random.Range(0, 100);

        if(random < 5)
        {
            Debug.Log("金");
            GatyaGold();
        }
        else if (random < 35)
        {
            Debug.Log("銀");
            GatyaSilver();
        }
        else
        {
            Debug.Log("銅");
            GatyaBronze();
        } 
    }
    public void GatyaBonus()
    {
        int random = Random.Range(0, 100);

        if (random < 5)
        {
            Debug.Log("金");
            GatyaGold();
        }
        else
        {
            Debug.Log("銀");
            GatyaSilver();
        }
    }
    public void GatyaGold()
    {
        int total = 0;
        // 敵ドロップ用の辞書からドロップ率を合計する
        foreach (KeyValuePair<AllyStatus, int> elem in gatyalist.GetGold())
        {
            total = elem.Value+1;
        }
        //Debug.Log(total);

        int randomgold = (int)(Random.value*total);
        //Debug.Log(randomgold);

        // randomPointの位置に該当するキーを返す
        foreach (KeyValuePair<AllyStatus, int> elem in gatyalist.GetGold())
        {
            if (randomgold == elem.Value)
            {
                Debug.Log(elem.Key);
                ally = elem.Key;
            } 
        }
        resulttext.text += "☆" + ally.characterName;
        if (allys.ContainsKey(ally))
        {
            playerdata.star += 100;
            resulttext.text += "：スターに変換しました"+"\n";
        }
        else
        {
            allys.Add(ally, 1);
            int a = alldata.allydictionary.FirstOrDefault(d => d.Key == ally).Value;
            playerdata.allydictionary.Add(a, 1);
            resulttext.text += "\n";
        }
    }
    public void GatyaSilver()
    {
        int total = 0;
        // 敵ドロップ用の辞書からドロップ率を合計する
        foreach (KeyValuePair<AllyStatus, int> elem in gatyalist.GetSilver())
        {
            total = elem.Value + 1;
        }
        //Debug.Log(total);

        int randomsilver = (int)(Random.value * total);
        //Debug.Log(randomgold);

        // randomPointの位置に該当するキーを返す
        foreach (KeyValuePair<AllyStatus, int> elem in gatyalist.GetSilver())
        {
            if (randomsilver == elem.Value)
            {
                Debug.Log(elem.Key);
                ally = elem.Key;
            }
        }
        resulttext.text += "○" + ally.characterName;
        if (allys.ContainsKey(ally))
        {
            playerdata.star += 5;
            resulttext.text += "：スターに変換しました" + "\n";
        }
        else
        {
            allys.Add(ally, 1);
            int a = alldata.allydictionary.FirstOrDefault(d => d.Key == ally).Value;
            playerdata.allydictionary.Add(a, 1);
            resulttext.text += "\n";
        }
    }
    public void GatyaBronze()
    {
        int total = 0;
        // 敵ドロップ用の辞書からドロップ率を合計する
        foreach (KeyValuePair<AllyStatus, int> elem in gatyalist.GetGold())
        {
            total = elem.Value + 1;
        }
        //Debug.Log(total);

        int randombronze = (int)(Random.value * total);
        //Debug.Log(randomgold);

        // randomPointの位置に該当するキーを返す
        foreach (KeyValuePair<AllyStatus, int> elem in gatyalist.GetBronze())
        {
            if (randombronze == elem.Value)
            {
                Debug.Log(elem.Key);
                ally = elem.Key;
            }
        }
        resulttext.text += "　" + ally.characterName;
        if (allys.ContainsKey(ally))
        {
            playerdata.star += 1;
            resulttext.text += "：スターに変換しました" + "\n";
        }
        else
        {
            allys.Add(ally, 1);
            int a = alldata.allydictionary.FirstOrDefault(d => d.Key == ally).Value;
            playerdata.allydictionary.Add(a, 1);
            resulttext.text += "\n";
        }
    }
    public void ResultOpen()
    {
        ResultPanel.SetActive(true);
    }
    public void ResultClose()
    {
        ResultPanel.SetActive(false);
        resulttext.text = "";
    }
}