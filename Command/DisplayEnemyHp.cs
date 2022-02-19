using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEnemyHp : MonoBehaviour
{ 
    [SerializeField]public Text PlayerNameText;
    [SerializeField]public Slider PlayerHPSlider;
    [SerializeField]public GameObject player;

    private void Start()
    {
        if (player.tag == "Mine")
        {
            return;
        }
        else if (player.tag == "Player")
        {
            PlayerNameText.text = player.GetComponent<UnityChanScript>().ally.characterName;
        }
        else if (player.tag == "Enemy")
        {
            PlayerNameText.text = player.GetComponent<EnemyAI>().charaname;
        }
    }
    void Update()
    {
        if (player.tag == "Mine")
        {
            return;
        }
        else if (player.tag == "Player")
        {
            PlayerHPSlider.value = (float)player.GetComponent<UnityChanScript>().hp / player.GetComponent<UnityChanScript>().maxhp;
        }
        else if(player.tag== "Enemy")
        {
            PlayerHPSlider.value = (float)player.GetComponent<EnemyAI>().hp / player.GetComponent<EnemyAI>().maxhp;
        }
    }
    void LateUpdate()
    {
        //　カメラと同じ向きに設定
        transform.rotation = Camera.main.transform.rotation;
    }
}
