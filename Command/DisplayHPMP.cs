using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayHPMP : MonoBehaviour
{
    //　パーティーステータス
    
    public AllyStatus allystatus;
    [SerializeField]
    private Slider hpslider;
    [SerializeField]
    private Slider mpslider;
    [SerializeField]
    private Slider stslider;
    
    public GameObject player;

    //　ステータスデータの表示
    public void DisplayStatus()
    {
        hpslider.value = (float)allystatus.hp / allystatus.maxHp;
        mpslider.value = (float)allystatus.mp / allystatus.maxMp;
        stslider.value = (float)allystatus.st / allystatus.maxSt;
    }
    private void Update()
    {
        if (player != null)
        {
            DisplayStatus();
        }
    }

}
